using MyBooseAppFramework.Interfaces;

namespace MyBooseAppFramework.Commands
{
    /// <summary>
    /// Command to draw a rectangle at the current pen position.
    /// </summary>
    public class RectCommand : ICommand
    {
        private readonly string[] _args;
        public string Name => "rect";

        public RectCommand(string[] args)
        {
            _args = args;
        }

        public void Execute(IBooseRuntime runtime)
        {
            int width = int.Parse(_args[0]);
            int height = int.Parse(_args[1]);

            runtime.Commands.Add(
                $"rect {runtime.Pen.X},{runtime.Pen.Y},{width},{height},{runtime.PenColor.R},{runtime.PenColor.G},{runtime.PenColor.B}");
        }
    }
}
