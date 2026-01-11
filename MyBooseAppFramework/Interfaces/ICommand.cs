namespace MyBooseAppFramework.Interfaces
{
    public interface ICommand
    {
        string Name { get; }
        void Execute();
    }
}
