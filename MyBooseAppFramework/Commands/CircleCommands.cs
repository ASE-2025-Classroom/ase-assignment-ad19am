using MyBooseAppFramework.Interfaces;

namespace MyBooseAppFramework.Commands
{
    /// <summary>
    /// Command to draw a circle.
    /// </summary>
    public class CircleCommand : ICommand
    {
        private readonly string[] _args;
        public string Name => "circle";

        public CircleCommand(string[] args) => _args = args;

        public void Execute()
        {
            BooseContext.Instance.Output?.WriteLine($"Executing circle {string.Join(",", _args)}");

        }
    }
}
