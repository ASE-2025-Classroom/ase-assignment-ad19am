using MyBooseAppFramework.Interfaces;

namespace MyBooseAppFramework.Commands
{
    /// <summary>
    /// Command to move the pen to a new position without drawing.
    /// </summary>
    public class MoveToCommand : ICommand
    {
        private readonly string[] _args;
        public string Name => "moveto";

        public MoveToCommand(string[] args)
        {
            _args = args;
        }

        public void Execute(IBooseRuntime runtime)
        {
            int x = int.Parse(_args[0]);
            int y = int.Parse(_args[1]);

            runtime.Pen.MoveTo(x, y);

            runtime.Commands.Add($"moveto {x},{y}");
        }
    }
}
