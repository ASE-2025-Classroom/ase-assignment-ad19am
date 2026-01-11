namespace MyBooseAppFramework.Interfaces
{
    public interface IVariableStore
    {
        bool Exists(string name);

        void SetScalar(string name, double value);
        double GetScalar(string name);

        void DeclareArray(string name, int size);
        void SetArrayValue(string name, int index, double value);
        double GetArrayValue(string name, int index);

        void PushScope();
        void PopScope();
    }
}
