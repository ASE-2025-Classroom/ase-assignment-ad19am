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
            _line = line;
        }

        public void Execute(IBooseRuntime runtime)
        {
            var vars = BooseContext.Instance.Variables;
            if (vars == null)
                throw new InvalidOperationException("VariableStore not initialised.");

            var runner = runtime as BooseProgramRunner;
            if (runner == null)
                throw new InvalidOperationException("SetVariableCommand requires BooseProgramRunner runtime.");

            var parts = _line.Split(new[] { '=' }, 2);
            if (parts.Length != 2)
                throw new FormatException("Invalid assignment syntax. Use: name = value");

            string left = parts[0].Trim();
            string right = parts[1].Trim();

            if (right.StartsWith("array", StringComparison.OrdinalIgnoreCase))
            {
                var rParts = right.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (rParts.Length != 2) throw new FormatException("Array declaration must be: name = array size");

                int size = (int)runner.ResolveValue(rParts[1]);
                vars.DeclareArray(left, size);
                return;
            }

            if (left.Contains("[") && left.EndsWith("]"))
            {
                int open = left.IndexOf('[');
                string name = left.Substring(0, open).Trim();
                string indexStr = left.Substring(open + 1, left.Length - open - 2);

                int index = (int)runner.ResolveValue(indexStr);
                double value = EvaluateExpression(runner, right);

                vars.SetArrayValue(name, index, value);
                return;
            }

            vars.SetScalar(left, EvaluateExpression(runner, right));
        }

        private double EvaluateExpression(BooseProgramRunner runner, string expr)
        {
            string[] ops = { "+", "-", "*", "/" };

            foreach (var op in ops)
            {
                int idx = expr.IndexOf(op, StringComparison.Ordinal);
                if (idx > 0)
                {
                    string a = expr.Substring(0, idx).Trim();
                    string b = expr.Substring(idx + 1).Trim();

                    double x = runner.ResolveValue(a);
                    double y = runner.ResolveValue(b);

                    switch (op)
                    {
                        case "+": return x + y;
                        case "-": return x - y;
                        case "*": return x * y;
                        case "/": return x / y;
                    }
                }
            }

            return runner.ResolveValue(expr);
        }
    }
}