using MyBooseAppFramework.Interfaces;

namespace MyBooseAppFramework.Commands
{
    /// <summary>
    /// Command to draw a line from the current pen position to a new position.
    /// </summary>
    public class DrawToCommand : ICommand
    {
        private readonly string[] _args;
        public string Name => "drawto";

        public DrawToCommand(string[] args)
        {
            _args = args;
        }

        public void Execute(IBooseRuntime runtime)
        {
            int x = int.Parse(_args[0]);
            int y = int.Parse(_args[1]);

            runtime.Pen.DrawTo(x, y);

            runtime.Commands.Add(
                $"drawto {x},{y},{runtime.PenColor.R},{runtime.PenColor.G},{runtime.PenColor.B}");
        }
    }
}
