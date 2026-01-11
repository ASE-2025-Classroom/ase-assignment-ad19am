using MyBooseAppFramework.Interfaces;

namespace MyBooseAppFramework
{
    public sealed class BooseContext
    {
        private static readonly BooseContext _instance = new BooseContext();
        public static BooseContext Instance => _instance;

        private BooseContext() { }

        public IVariableStore Variables { get; set; }

        public IOutput Output { get; set; }
    }
}
