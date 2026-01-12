using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyBooseAppFramework;
using MyBooseAppFramework.Commands;
using MyBooseAppFramework.Stores;

namespace MyBooseAppTests
{
    /// <summary>
    /// Unit tests for VariableStore.
    /// </summary>
    [TestClass]
    public class VariableStoreTests
    {
        /// <summary>
        /// Verifies setting and getting a scalar variable works.
        /// </summary>
        [TestMethod]
        public void SetScalar_ThenGetScalar_ReturnsValue()
        {
            var store = new VariableStore();

            store.SetScalar("x", 10);

            Assert.AreEqual(10, store.GetScalar("x"));
        }

        /// <summary>
        /// Verifies array values can be set and retrieved.
        /// This mirrors how the interpreter uses arrays.
        /// </summary>
        [TestMethod]
        public void Array_CreationAndAccess_WorksViaBooseSyntax()
        {
            var runner = new BooseProgramRunner();

            BooseContext.Instance.Variables = new MyBooseAppFramework.Stores.VariableStore();

            string program = @"arr = array 3
arr[0] = 20
arr[1] = 40
arr[2] = 60";

            runner.Run(program);

            var vars = BooseContext.Instance.Variables;
            Assert.AreEqual(20, vars.GetArrayValue("arr", 0));
            Assert.AreEqual(40, vars.GetArrayValue("arr", 1));
            Assert.AreEqual(60, vars.GetArrayValue("arr", 2));
        }

        /// <summary>
        /// Verifies unknown scalar access throws.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.Collections.Generic.KeyNotFoundException))]
        public void GetScalar_UnknownVariable_Throws()
        {
            var store = new VariableStore();
            store.GetScalar("doesNotExist");
        }
    }
}