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
            BooseContext.Instance.Variables = new VariableStore();

            var create = new SetVariableCommand("arr = array 3");
            create.Execute(null);

            var set0 = new SetVariableCommand("arr[0] = 20");
            set0.Execute(null);

            var set1 = new SetVariableCommand("arr[1] = 40");
            set1.Execute(null);

            Assert.AreEqual(20, BooseContext.Instance.Variables.GetArrayValue("arr", 0));
            Assert.AreEqual(40, BooseContext.Instance.Variables.GetArrayValue("arr", 1));
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