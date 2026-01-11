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

        public MoveToCommand(string[] args) => _args = args;

        public void Execute()
        {
            BooseContext.Instance.Output?.WriteLine($"Executing moveto {string.Join(",", _args)}");

        }
    }
}
