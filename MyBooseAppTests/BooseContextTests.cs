using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyBooseAppFramework;
using MyBooseAppFramework.Interfaces;

namespace MyBooseAppTests
{
    /// <summary>
    /// Unit tests for BooseContext.
    /// </summary>
    [TestClass]
    public class BooseContextTests
    {
        /// <summary>
        /// Verifies BooseContext implements a singleton.
        /// </summary>
        [TestMethod]
        public void Instance_ReturnsSameObject()
        {
            var a = BooseContext.Instance;
            var b = BooseContext.Instance;

            Assert.AreSame(a, b);
        }

        /// <summary>
        /// Verifies the Variables property can be set and retrieved.
        /// </summary>
        [TestMethod]
        public void Variables_CanBeSet()
        {
            IVariableStore store = new MyBooseAppFramework.Stores.VariableStore();
            BooseContext.Instance.Variables = store;

            Assert.AreSame(store, BooseContext.Instance.Variables);
        }

        /// <summary>
        /// Verifies the Output property can be set and retrieved.
        /// </summary>
        [TestMethod]
        public void Output_CanBeSet()
        {
            IOutput output = new DummyOutput();
            BooseContext.Instance.Output = output;

            Assert.AreSame(output, BooseContext.Instance.Output);
        }

        private class DummyOutput : IOutput
        {
            public void WriteLine(string message) { /* no-op */ }
        }
    }
}