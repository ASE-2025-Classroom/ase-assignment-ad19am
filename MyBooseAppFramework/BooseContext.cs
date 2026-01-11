using MyBooseAppFramework.Interfaces;

namespace MyBooseAppFramework
{
    /// <summary>
    /// Singleton context holding shared interpreter services.
    /// </summary>
    public sealed class BooseContext
    {
        private static readonly BooseContext _instance = new BooseContext();
        public static BooseContext Instance => _instance;

        private BooseContext() { }

        /// <summary>
        /// Variable store used by the interpreter.
        /// </summary>
        public IVariableStore Variables { get; set; }

        /// <summary>
        /// Output taregt.
        /// </summary>
        public IOutput Output { get; set; }
    }
}
