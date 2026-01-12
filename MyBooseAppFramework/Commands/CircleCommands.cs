using System;
using MyBooseAppFramework.Interfaces;

namespace MyBooseAppFramework.Commands

{/// <summary>
/// Command to draw a circle at the current pen position.
/// </summary>
    public class CircleCommand : ICommand
    {
        private readonly string[] _args;
        public string Name => "circle";

        public CircleCommand(string[] args)
        {
            _args = args ?? Array.Empty<string>();
        }

        public void Execute(IBooseRuntime runtime)
        {
            if (runtime == null) throw new ArgumentNullException(nameof(runtime));
            if (_args.Length < 1) throw new FormatException("circle requires 1 value: radius");

            int radius = (int)runtime.ResolveValue(_args[0]);

            runtime.Commands.Add(
                $"circle {runtime.Pen.X},{runtime.Pen.Y},{radius},{runtime.PenColor.R},{runtime.PenColor.G},{runtime.PenColor.B}"
            );
        }
    }
}