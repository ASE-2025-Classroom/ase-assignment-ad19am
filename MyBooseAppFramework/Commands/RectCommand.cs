using System;
using MyBooseAppFramework.Interfaces;

namespace MyBooseAppFramework.Commands
/// <summary>
/// Command to draw a rectangle at the current pen position.
/// </summary>
{
    public class RectCommand : ICommand
    {
        private readonly string[] _args;
        public string Name => "rect";

        public RectCommand(string[] args)
        {
            _args = args ?? Array.Empty<string>();
        }

        public void Execute(IBooseRuntime runtime)
        {
            if (runtime == null) throw new ArgumentNullException(nameof(runtime));
            if (_args.Length < 2) throw new FormatException("rect requires 2 values: width,height");

            int w = (int)runtime.ResolveValue(_args[0]);
            int h = (int)runtime.ResolveValue(_args[1]);

            runtime.Commands.Add(
                $"rect {runtime.Pen.X},{runtime.Pen.Y},{w},{h},{runtime.PenColor.R},{runtime.PenColor.G},{runtime.PenColor.B}"
            );
        }
    }
}