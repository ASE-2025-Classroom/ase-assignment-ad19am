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
            var runner = (BooseProgramRunner)runtime;

            int w = (int)runner.ResolveValue(_args[0]);
            int h = (int)runner.ResolveValue(_args[1]);

            runtime.Commands.Add(
                $"rect {runtime.Pen.X},{runtime.Pen.Y},{w},{h},{runtime.PenColor.R},{runtime.PenColor.G},{runtime.PenColor.B}");
        }
    }
}
