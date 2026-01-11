using System;
using MyBooseAppFramework.Interfaces;
using MyBooseAppFramework.Commands;

namespace MyBooseAppFramework.Factories
{
    public class CommandFactory : ICommandFactory
    {
        public ICommand Create(string keyword, string[] args)
        {
            keyword = (keyword ?? "").Trim().ToLowerInvariant();

            switch (keyword)
            {
                case "moveto":
                    return new MoveToCommand(args);

                case "drawto":
                    return new DrawToCommand(args); // only if you created DrawToCommand

                case "circle":
                    return new CircleCommand(args);

                case "rect":
                    return new RectCommand(args);

                case "pencolour":
                case "pencolor":
                    return new PenColourCommand(args);

                case "write":
                    return new WriteCommand(args);

                default:
                    throw new ArgumentException($"Unknown command: {keyword}");
            }
        }
    }
}
