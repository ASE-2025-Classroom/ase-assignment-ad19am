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
            if (vars == null) throw new InvalidOperationException("VariableStore not initialised.");

            var parts = _line.Split(new[] { '=' }, 2);
            if (parts.Length != 2) throw new FormatException("Invalid assignment. Use: name = value");

            string left = parts[0].Trim();
            string right = parts[1].Trim();

            if (right.StartsWith("array", StringComparison.OrdinalIgnoreCase))
            {
                var rParts = right.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (rParts.Length != 2) throw new FormatException("Array declaration must be: name = array size");
                int size = int.Parse(rParts[1]);
                vars.DeclareArray(left, size);
                return;
            }

            if (left.Contains("[") && left.EndsWith("]"))
            {
                int open = left.IndexOf('[');
                string name = left.Substring(0, open).Trim();
                string indexStr = left.Substring(open + 1, left.Length - open - 2);
                int index = int.Parse(indexStr);

                double value = double.Parse(right);
                vars.SetArrayValue(name, index, value);
                return;
            }

            vars.SetScalar(left, double.Parse(right));
        }
    }
}
