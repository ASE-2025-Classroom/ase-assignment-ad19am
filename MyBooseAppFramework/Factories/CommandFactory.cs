using MyBooseAppFramework.Interfaces;
using System;
using System.Windows.Input;

namespace MyBooseAppFramework.Factories
{
    /// <summary>
    /// Factory pattern - creates ICommand instances from BOOSE keywords.
    /// </summary>
    public class CommandFactory : ICommandFactory
    {
        public ICommand Create(string keyword, string[] args)
        {
            keyword = (keyword ?? "").Trim().ToLowerInvariant();

            switch (keyword)
            {

                case "moveto":
                    return new Commands.MoveToCommand(args);

                case "rect":
                    return new Commands.RectCommand(args);

                case "circle":
                    return new Commands.CircleCommand(args);

                case "pencolour":
                case "pencolor":
                    return new Commands.PenColourCommand(args);

                case "write":
                    return new Commands.WriteCommand(args);

                default:
                    throw new ArgumentException($"Unknown command: {keyword}");
            }
        }
    }
}
