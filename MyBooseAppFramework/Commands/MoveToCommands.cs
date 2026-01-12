using System;
using MyBooseAppFramework.Interfaces;

namespace MyBooseAppFramework.Commands
/// <summary>
/// Command to move the pen to a new position without drawing.
/// </summary>
{
    public class MoveToCommand : ICommand
    {
        private readonly string[] _args;
        public string Name => "moveto";

        public MoveToCommand(string[] args)
        {
            _args = args ?? Array.Empty<string>();
        }

        public void Execute(IBooseRuntime runtime)
        {
            if (runtime == null) throw new ArgumentNullException(nameof(runtime));
            if (_args.Length < 2) throw new FormatException("moveto requires 2 values: x,y");

            int x = (int)runtime.ResolveValue(_args[0]);
            int y = (int)runtime.ResolveValue(_args[1]);

            runtime.Pen.MoveTo(x, y);
        }
    }
}