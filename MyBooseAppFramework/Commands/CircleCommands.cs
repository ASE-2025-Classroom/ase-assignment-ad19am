using MyBooseAppFramework.Interfaces;

namespace MyBooseAppFramework.Commands
{
    /// <summary>
    /// Command to draw a circle at the current pen position.
    /// </summary>
    public class CircleCommand : ICommand
    {
        private readonly string[] _args;
        public string Name => "circle";

        public CircleCommand(string[] args) => _args = args;

        public void Execute(IBooseRuntime runtime)
        {
            var runner = (BooseProgramRunner)runtime;

            int radius = (int)runner.ResolveValue(_args[0]);

            runtime.Commands.Add(
                $"circle {runtime.Pen.X},{runtime.Pen.Y},{radius},{runtime.PenColor.R},{runtime.PenColor.G},{runtime.PenColor.B}");
        }
    }
}
