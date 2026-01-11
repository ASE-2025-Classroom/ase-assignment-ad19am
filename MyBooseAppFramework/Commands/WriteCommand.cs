using MyBooseAppFramework.Interfaces;

namespace MyBooseAppFramework.Commands
{
    /// <summary>
    /// Command to write text at the current pen position.
    /// </summary>
    public class WriteCommand : ICommand
    {
        private readonly string[] _args;
        public string Name => "write";

        public WriteCommand(string[] args)
        {
            _args = args;
        }

        public void Execute(IBooseRuntime runtime)
        {
            string text = string.Join(" ", _args).Trim().Trim('"');

            runtime.Commands.Add(
                $"write {runtime.Pen.X},{runtime.Pen.Y},\"{text}\",{runtime.PenColor.R},{runtime.PenColor.G},{runtime.PenColor.B}");
        }
    }
}
