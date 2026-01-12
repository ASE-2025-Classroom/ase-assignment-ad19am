using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyBooseAppFramework;
using MyBooseAppFramework.Commands;
using MyBooseAppFramework.Stores;

namespace MyBooseAppTests
{
    /// <summary>
    /// Unit tests for SetVariableCommand.
    /// </summary>
    [TestClass]
    public class SetVariableCommandTests
    {
        /// <summary>
        /// Verifies scalar assignment sets the variable in the store.
        /// </summary>
        [TestMethod]
        public void Execute_SetsScalarVariable()
        {
            BooseContext.Instance.Variables = new VariableStore();

            var runner = new BooseProgramRunner();
            var cmd = new SetVariableCommand("x = 5");

            cmd.Execute(runner);

            Assert.AreEqual(5, BooseContext.Instance.Variables.GetScalar("x"));
        }

        /// <summary>
        /// Verifies array creation syntax allocates an array.
        /// </summary>
        [TestMethod]
        public void Execute_ArrayCreation_Works()
        {
            BooseContext.Instance.Variables = new VariableStore();

            var runner = new BooseProgramRunner();
            var cmd = new SetVariableCommand("arr = array 3");

            cmd.Execute(runner);

            Assert.AreEqual(0, BooseContext.Instance.Variables.GetArrayValue("arr", 0));
        }

        /// <summary>
        /// Verifies array index assignment sets a value.
        /// </summary>
        [TestMethod]
        public void Execute_ArrayIndexAssignment_Works()
        {
            BooseContext.Instance.Variables = new VariableStore();

            var runner = new BooseProgramRunner();

            new SetVariableCommand("arr = array 3").Execute(runner);
            new SetVariableCommand("arr[1] = 40").Execute(runner);

            Assert.AreEqual(40, BooseContext.Instance.Variables.GetArrayValue("arr", 1));
        }
    }
}