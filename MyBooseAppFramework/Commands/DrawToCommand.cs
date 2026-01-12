using System;
using MyBooseAppFramework.Interfaces;

namespace MyBooseAppFramework.Commands
/// <summary>
/// Command to draw a line from the current pen position to a new position.
/// </summary>
{
    public class DrawToCommand : ICommand
    {
        private readonly string[] _args;
        public string Name => "drawto";

        public DrawToCommand(string[] args)
        {
            _args = args ?? Array.Empty<string>();
        }

        public void Execute(IBooseRuntime runtime)
        {
            if (runtime == null) throw new ArgumentNullException(nameof(runtime));
            if (_args.Length < 2) throw new FormatException("drawto requires 2 values: x,y");

            int x = (int)runtime.ResolveValue(_args[0]);
            int y = (int)runtime.ResolveValue(_args[1]);

            runtime.Pen.DrawTo(x, y);

            runtime.Commands.Add(
                $"drawto {x},{y},{runtime.PenColor.R},{runtime.PenColor.G},{runtime.PenColor.B}"
            );
        }
    }
}