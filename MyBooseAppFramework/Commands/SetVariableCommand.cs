using System;
using MyBooseAppFramework.Interfaces;

namespace MyBooseAppFramework.Commands
{
    public class SetVariableCommand : ICommand
    {
        private readonly string _line;
        public string Name => "setvar";

        public SetVariableCommand(string line)
        {
            _line = line ?? throw new ArgumentNullException(nameof(line));
        }

        public void Execute(IBooseRuntime runtime)
        {
            if (runtime == null) throw new ArgumentNullException(nameof(runtime));

            var vars = BooseContext.Instance.Variables;
            if (vars == null)
                throw new InvalidOperationException("VariableStore not initialised.");

            var parts = _line.Split(new[] { '=' }, 2);
            if (parts.Length != 2)
                throw new FormatException("Invalid assignment syntax. Use: name = value");

            string left = parts[0].Trim();
            string right = parts[1].Trim();

            if (right.StartsWith("array", StringComparison.OrdinalIgnoreCase))
            {
                var rParts = right.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (rParts.Length != 2)
                    throw new FormatException("Array declaration must be: name = array size");

                int size = (int)runtime.ResolveValue(rParts[1]);
                vars.DeclareArray(left, size);
                return;
            }

            if (left.Contains("[") && left.EndsWith("]"))
            {
                int open = left.IndexOf('[');
                if (open <= 0)
                    throw new FormatException("Invalid array syntax on left side.");

                string name = left.Substring(0, open).Trim();
                string indexStr = left.Substring(open + 1, left.Length - open - 2).Trim();

                int index = (int)runtime.ResolveValue(indexStr);
                double value = EvaluateExpression(runtime, right);

                vars.SetArrayValue(name, index, value);
                return;
            }

            vars.SetScalar(left, EvaluateExpression(runtime, right));
        }

        private static double EvaluateExpression(IBooseRuntime runtime, string expr)
        {
            if (string.IsNullOrWhiteSpace(expr))
                throw new FormatException("Missing expression on right side.");

            expr = expr.Trim();

            string[] ops = { "+", "-", "*", "/" };

            foreach (var op in ops)
            {
                int idx = expr.IndexOf(op, StringComparison.Ordinal);
                if (idx > 0)
                {
                    string a = expr.Substring(0, idx).Trim();
                    string b = expr.Substring(idx + 1).Trim();

                    double x = runtime.ResolveValue(a);
                    double y = runtime.ResolveValue(b);

                    switch (op)
                    {
                        case "+": return x + y;
                        case "-": return x - y;
                        case "*": return x * y;
                        case "/": return x / y;
                    }
                }
            }

            return runtime.ResolveValue(expr);
        }
    }
}