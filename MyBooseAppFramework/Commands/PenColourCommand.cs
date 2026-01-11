using MyBooseAppFramework.Interfaces;

namespace MyBooseAppFramework.Commands
{
    /// <summary>
    /// Command to change pen colour.
    /// </summary>
    public class PenColourCommand : ICommand
    {
        private readonly string[] _args;
        public string Name => "pencolour";

        public PenColourCommand(string[] args) => _args = args;

        public void Execute()
        {
            BooseContext.Instance.Output?.WriteLine($"Executing pencolour {string.Join(",", _args)}");

        }
    }
}