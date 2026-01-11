using MyBooseAppFramework.Interfaces;

namespace MyBooseAppFramework.Commands
{
    /// <summary>
    /// Command to change pen colour.
    /// </summary>
    public class PenColorCommand : ICommand
    {
        private readonly string[] _args;
        public string Name => "pencolor";

        public PenColorCommand(string[] args) => _args = args;

        public void Execute()
        {
            BooseContext.Instance.Output?.WriteLine($"Executing pencolor {string.Join(",", _args)}");

        }
    }
}