using MyBooseAppFramework.Interfaces;
using MyBooseAppFramework.Factories;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace MyBooseAppFramework
{
    /// <summary>
    /// Executes simple BOOSE programs consisting of moveto, drawto and
    /// basic drawing commands. Maintains a single BoosePen instance.
    /// </summary>
    public class BooseProgramRunner : IBooseRuntime
    {   
        private readonly ICommandFactory _factory = new CommandFactory();

        /// <summary>
        /// Returns information about this BOOSE library, used for the About display.
        /// </summary>
        public static string About()
        {
            return "MyBooseAppFramework - simple BOOSE interpreter for ASE assignment.";
        }

        /// <summary>
        /// The pen used by this runner to track the current coordinates.
        /// </summary>
        public BoosePen Pen { get; }

        /// <summary>
        /// Current pen colour.
        /// </summary>
        public Color PenColor { get; set; } = Color.Black;

        /// <summary>
        /// List of drawing commands produced while running the program.
        /// Each entry is a simple string that the UI can parse and draw.
        /// </summary>
        public List<string> Commands { get; } = new List<string>();

        /// <summary>
        /// Creates a new program runner with a fresh pen at (0,0).
        /// </summary>
        public BooseProgramRunner()
        {
            Pen = new BoosePen();
            PenColor = Color.Black;

            BooseContext.Instance.Variables = new MyBooseAppFramework.Stores.VariableStore();
        }

        /// <summary>
        /// Runs a BOOSE program made up of commands (one per line).
        /// Supports: moveto, drawto, pencolour, circle, rect, write.
        /// </summary>
        /// <param name="programText">
        /// The text of the program. Lines such as 'moveto 10,10' or 'circle 50'.
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown if programText is null.</exception>
        /// <exception cref="BooseSyntaxException">
        /// Thrown when the program contains unknown commands or badly formatted coordinates.
        /// </exception>
        /// <exception cref="BooseRuntimeException">
        /// Thrown when an unexpected runtime error occurs while executing the program.
        /// </exception>
        public void Run(string programText)
        {
            if (programText == null)
                throw new ArgumentNullException(nameof(programText));

            Commands.Clear();

            var rawLines = programText.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
            var lines = new List<string>();

            foreach (var rl in rawLines)
            {
                var ln = rl.Trim();
                if (string.IsNullOrWhiteSpace(ln)) continue;
                if (ln.StartsWith("//")) continue;
                lines.Add(ln);
            }

            var ifStack = new Stack<bool>();
            var whileStack = new Stack<int>();
            var forStack = new Stack<(string varName, double end, double step, int forLineIndex)>();

            int pc = 0;
            int safety = 0;

            while (pc < lines.Count)
            {
                safety++;
                if (safety > 200000) throw new BooseRuntimeException("Safety stop: possible infinite loop.");

                string line = lines[pc];

                try
                {
                    if (line.StartsWith("if ", StringComparison.OrdinalIgnoreCase))
                    {
                        string cond = line.Substring(3).Trim();
                        bool ok = EvaluateCondition(cond);
                        ifStack.Push(ok);

                        if (!ok)
                        {
                            int depth = 0;
                            int j = pc + 1;
                            for (; j < lines.Count; j++)
                            {
                                string t = lines[j];
                                if (t.StartsWith("if ", StringComparison.OrdinalIgnoreCase)) depth++;
                                else if (t.Equals("endif", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (depth == 0) break;
                                    depth--;
                                }
                                else if (t.Equals("else", StringComparison.OrdinalIgnoreCase) && depth == 0)
                                {
                                    break;
                                }
                            }
                            pc = j;
                        }
                        pc++;
                        continue;
                    }

                    if (line.Equals("else", StringComparison.OrdinalIgnoreCase))
                    {
                        if (ifStack.Count == 0) throw new BooseSyntaxException("else without matching if");

                        bool ifWasTrue = ifStack.Peek();
                        if (ifWasTrue)
                        {
                            int depth = 0;
                            int j = pc + 1;
                            for (; j < lines.Count; j++)
                            {
                                string t = lines[j];
                                if (t.StartsWith("if ", StringComparison.OrdinalIgnoreCase)) depth++;
                                else if (t.Equals("endif", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (depth == 0) break;
                                    depth--;
                                }
                            }
                            pc = j + 1;
                            ifStack.Pop();
                            continue;
                        }
                        else
                        {
                            pc++;
                            continue;
                        }
                    }

                    if (line.Equals("endif", StringComparison.OrdinalIgnoreCase))
                    {
                        if (ifStack.Count == 0) throw new BooseSyntaxException("endif without matching if");
                        ifStack.Pop();
                        pc++;
                        continue;
                    }

                    if (line.StartsWith("while ", StringComparison.OrdinalIgnoreCase))
                    {
                        string cond = line.Substring(6).Trim();
                        bool ok = EvaluateCondition(cond);

                        if (!ok)
                        {
                            int depth = 0;
                            int j = pc + 1;
                            for (; j < lines.Count; j++)
                            {
                                string t = lines[j];
                                if (t.StartsWith("while ", StringComparison.OrdinalIgnoreCase)) depth++;
                                else if (t.Equals("endwhile", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (depth == 0) break;
                                    depth--;
                                }
                            }
                            pc = j + 1;
                        }
                        else
                        {
                            whileStack.Push(pc);
                            pc++;
                        }
                        continue;
                    }

                    if (line.Equals("endwhile", StringComparison.OrdinalIgnoreCase))
                    {
                        if (whileStack.Count == 0) throw new BooseSyntaxException("endwhile without matching while");

                        int whileLineIndex = whileStack.Peek();
                        string whileLine = lines[whileLineIndex];
                        string cond = whileLine.Substring(6).Trim();

                        if (EvaluateCondition(cond))
                        {
                            pc = whileLineIndex + 1;
                        }
                        else
                        {
                            whileStack.Pop();
                            pc++;
                        }
                        continue;
                    }

                    if (line.StartsWith("for ", StringComparison.OrdinalIgnoreCase))
                    {
                        string rest = line.Substring(4).Trim();

                        var eqParts = rest.Split(new[] { '=' }, 2);
                        if (eqParts.Length != 2) throw new FormatException("for syntax must be: for i = start to end");

                        string varName = eqParts[0].Trim();
                        string rhs = eqParts[1].Trim();

                        var toParts = rhs.Split(new[] { "to" }, StringSplitOptions.RemoveEmptyEntries);
                        if (toParts.Length < 2) throw new FormatException("for syntax must include: to");

                        string startExpr = toParts[0].Trim();
                        string endAndStep = toParts[1].Trim();

                        double start = ResolveValue(startExpr);

                        double step = 1;
                        double end;

                        int stepPos = endAndStep.IndexOf("step", StringComparison.OrdinalIgnoreCase);
                        if (stepPos >= 0)
                        {
                            string endExpr = endAndStep.Substring(0, stepPos).Trim();
                            string stepExpr = endAndStep.Substring(stepPos + 4).Trim();
                            end = ResolveValue(endExpr);
                            step = ResolveValue(stepExpr);
                            if (Math.Abs(step) < 0.000001) throw new FormatException("for step cannot be 0");
                        }
                        else
                        {
                            end = ResolveValue(endAndStep);
                        }

                        BooseContext.Instance.Variables.SetScalar(varName, start);

                        bool run =
                            (step > 0 && start <= end) ||
                            (step < 0 && start >= end);

                        if (!run)
                        {
                            int depth = 0;
                            int j = pc + 1;
                            for (; j < lines.Count; j++)
                            {
                                string t = lines[j];
                                if (t.StartsWith("for ", StringComparison.OrdinalIgnoreCase)) depth++;
                                else if (t.Equals("endfor", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (depth == 0) break;
                                    depth--;
                                }
                            }
                            pc = j + 1;
                        }
                        else
                        {
                            forStack.Push((varName, end, step, pc));
                            pc++;
                        }
                        continue;
                    }

                    if (line.Equals("endfor", StringComparison.OrdinalIgnoreCase))
                    {
                        if (forStack.Count == 0) throw new BooseSyntaxException("endfor without matching for");

                        var frame = forStack.Peek();
                        double current = BooseContext.Instance.Variables.GetScalar(frame.varName);
                        double next = current + frame.step;

                        BooseContext.Instance.Variables.SetScalar(frame.varName, next);

                        bool keepGoing =
                            (frame.step > 0 && next <= frame.end) ||
                            (frame.step < 0 && next >= frame.end);

                        if (keepGoing)
                        {
                            pc = frame.forLineIndex + 1;
                        }
                        else
                        {
                            forStack.Pop();
                            pc++;
                        }
                        continue;
                    }

                    if (IsAssignmentLine(line))
                    {
                        var varCmd = new MyBooseAppFramework.Commands.SetVariableCommand(line);
                        varCmd.Execute(this);
                        pc++;
                        continue;
                    }

                    int firstSpace = line.IndexOf(' ');
                    string keyword = firstSpace < 0 ? line : line.Substring(0, firstSpace);
                    string argString = firstSpace < 0 ? "" : line.Substring(firstSpace + 1);

                    string[] args = argString.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    MyBooseAppFramework.Interfaces.ICommand cmd = _factory.Create(keyword, args);
                    cmd.Execute(this);

                    pc++;
                }
                catch (FormatException ex)
                {
                    throw new BooseSyntaxException($"Line {pc + 1}: {ex.Message}", ex);
                }
                catch (BooseSyntaxException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    throw new BooseRuntimeException($"Runtime error near line {pc + 1}: {ex.Message}", ex);
                }
            }

            if (ifStack.Count > 0) throw new BooseSyntaxException("Unclosed IF block.");
            if (whileStack.Count > 0) throw new BooseSyntaxException("Unclosed WHILE block.");
            if (forStack.Count > 0) throw new BooseSyntaxException("Unclosed FOR block.");
        }

        private static bool IsAssignmentLine(string line)
        {
            if (!line.Contains("=")) return false;
            if (line.Contains("==") || line.Contains("!=") || line.Contains(">=") || line.Contains("<=")) return false;
            if (line.TrimStart().StartsWith("for ", StringComparison.OrdinalIgnoreCase)) return false;
            return true;
        }

        private bool EvaluateCondition(string condition)
        {
            string[] ops = new[] { "==", "!=", ">=", "<=", ">", "<" };

            string op = null;
            int opPos = -1;

            foreach (var candidate in ops)
            {
                opPos = condition.IndexOf(candidate, StringComparison.Ordinal);
                if (opPos >= 0) { op = candidate; break; }
            }

            if (op == null) throw new FormatException($"Invalid condition: {condition}");

            string left = condition.Substring(0, opPos).Trim();
            string right = condition.Substring(opPos + op.Length).Trim();

            double a = ResolveValue(left);
            double b = ResolveValue(right);

            switch (op)
            {
                case "==": return Math.Abs(a - b) < 0.000001;
                case "!=": return Math.Abs(a - b) >= 0.000001;
                case ">": return a > b;
                case "<": return a < b;
                case ">=": return a >= b;
                case "<=": return a <= b;
                default: throw new FormatException($"Unknown operator: {op}");
            }
        }

        public double ResolveValue(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new FormatException("Missing value.");

            token = token.Trim();

            double num;
            if (double.TryParse(token, out num))
                return num;

            var vars = BooseContext.Instance.Variables;
            if (vars == null)
                throw new InvalidOperationException("VariableStore not initialised.");

            if (token.Contains("[") && token.EndsWith("]"))
            {
                int open = token.IndexOf('[');
                string name = token.Substring(0, open).Trim();
                string indexStr = token.Substring(open + 1, token.Length - open - 2);

                int index = (int)ResolveValue(indexStr);
                return vars.GetArrayValue(name, index);
            }

            return vars.GetScalar(token);
        }

        /// <summary>
        /// Parses two integer coordinates from a command line, e.g. 'moveto 100,200'.
        /// </summary>
        /// <param name="line">The full line of text from the program.</param>
        /// <param name="command">The command name (moveto or drawto).</param>
        /// <param name="lineNumber">The current line number for error reporting.</param>
        /// <returns>A tuple containing the parsed X and Y coordinates.</returns>
        /// <exception cref="FormatException">
        /// Thrown when the coordinates cannot be parsed as two comma-separated integers.
        /// </exception>
        private static (int x, int y) ParseTwoInts(string line, string command, int lineNumber)
        {
            string argsPart = line.Substring(command.Length).Trim();

            var parts = argsPart.Split(',');

            if (parts.Length != 2)
            {
                throw new FormatException(
                    $"Line {lineNumber}: '{command}' requires two comma-separated numbers.");
            }

            if (!int.TryParse(parts[0].Trim(), out int x))
            {
                throw new FormatException(
                    $"Line {lineNumber}: Could not parse X coordinate in '{line}'.");
            }

            if (!int.TryParse(parts[1].Trim(), out int y))
            {
                throw new FormatException(
                    $"Line {lineNumber}: Could not parse Y coordinate in '{line}'.");
            }

            return (x, y);
        }
    }
}