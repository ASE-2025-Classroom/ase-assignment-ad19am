using MyBooseAppFramework.Interfaces;

namespace MyBooseAppFramework.Commands
{
    /// <summary>
    /// Command to draw a rectangle.
    /// </summary>
    public class RectCommand : ICommand
    {
        private readonly string[] _args;
        public string Name => "rect";

        public RectCommand(string[] args) => _args = args;

        public void Execute()
        {
            BooseContext.Instance.Output?.WriteLine($"Executing rectangle {string.Join(",", _args)}");

        }
    }
}