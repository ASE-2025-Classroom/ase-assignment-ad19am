using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyBooseAppFramework.Stores;

namespace MyBooseAppTests
{
    /// <summary>
    /// Unit tests for VariableStore
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
        /// Verifies array creation and retrieval works.
        /// </summary>
        [TestMethod]
        public void CreateArray_SetAndGetArrayValue_Works()
        {
            var store = new VariableStore();

            store.CreateArray("arr", 3);
            store.SetArrayValue("arr", 0, 20);
            store.SetArrayValue("arr", 1, 40);

            Assert.AreEqual(20, store.GetArrayValue("arr", 0));
            Assert.AreEqual(40, store.GetArrayValue("arr", 1));
        }

        /// <summary>
        /// Verifies GetScalar throws when variable not set.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.Exception))]
        public void GetScalar_UnknownVariable_Throws()
        {
            var store = new VariableStore();
            store.GetScalar("doesNotExist");
        }
    }
}