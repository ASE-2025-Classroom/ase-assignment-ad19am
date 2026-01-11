using System.Windows.Input;

namespace MyBooseAppFramework.Interfaces
{
    public interface ICommandFactory
    {
        ICommand Create(string keyword, string[] args);
    }
}
