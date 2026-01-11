using MyBooseAppFramework.Interfaces;

namespace MyBooseAppFramework.Commands
{

    public class WriteCommand : ICommand
    {
        private readonly string[] _args;
        public string Name => "write";

        public WriteCommand(string[] args) => _args = args;

        public void Execute()
        {
            BooseContext.Instance.Output?.WriteLine($"Executing write {string.Join(",", _args)}");

        }
    }
}