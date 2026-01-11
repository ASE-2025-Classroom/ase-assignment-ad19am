using MyBooseAppFramework.Interfaces;
using System.Drawing;

namespace MyBooseAppFramework.Commands
{
    /// <summary>
    /// Command to change pen colour.
    /// </summary>
    public class PenColourCommand : ICommand
    {
        private readonly string[] _args;
        public string Name => "pencolour";

        public PenColourCommand(string[] args)
        {
            _args = args;
        }

        public void Execute(IBooseRuntime runtime)
        {
            int r = int.Parse(_args[0]);
            int g = int.Parse(_args[1]);
            int b = int.Parse(_args[2]);

            runtime.PenColor = Color.FromArgb(r, g, b);

            runtime.Commands.Add($"pencolour {r},{g},{b}");
        }
    }
}
