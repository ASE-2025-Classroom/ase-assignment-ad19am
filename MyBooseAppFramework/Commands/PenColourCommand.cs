using System;
using System.Drawing;
using MyBooseAppFramework.Interfaces;

namespace MyBooseAppFramework.Commands
/// <summary>
/// Command to change pen colour.
/// </summary>
{
    public class PenColourCommand : ICommand
    {
        private readonly string[] _args;
        public string Name => "pencolour";

        public PenColourCommand(string[] args)
        {
            _args = args ?? Array.Empty<string>();
        }

        public void Execute(IBooseRuntime runtime)
        {
            if (runtime == null) throw new ArgumentNullException(nameof(runtime));
            if (_args.Length != 3) throw new FormatException("pencolour requires 3 values: r,g,b");

            int r = (int)runtime.ResolveValue(_args[0]);
            int g = (int)runtime.ResolveValue(_args[1]);
            int b = (int)runtime.ResolveValue(_args[2]);

            r = Clamp(r, 0, 255);
            g = Clamp(g, 0, 255);
            b = Clamp(b, 0, 255);

            runtime.PenColor = Color.FromArgb(r, g, b);
        }

        private static int Clamp(int value, int min, int max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }
    }
}