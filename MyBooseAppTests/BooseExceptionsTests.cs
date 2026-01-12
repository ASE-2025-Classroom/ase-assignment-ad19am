using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyBooseAppFramework;
using System;

namespace MyBooseAppTests
{
    [TestClass]
    public class BooseExceptionsTests
    {
        /// <summary>
        /// Verifies BooseSyntaxException stores the message passed into the constructor.
        /// </summary>
        [TestMethod]
        public void BooseSyntaxException_StoresMessage()
        {
            var ex = new BooseSyntaxException("bad syntax");
            Assert.AreEqual("bad syntax", ex.Message);
        }

        /// <summary>
        /// Verifies BooseRuntimeException stores the message and inner exception passed into the constructor.
        /// </summary>
        [TestMethod]
        public void BooseRuntimeException_StoresMessageAndInnerException()
        {
            var inner = new InvalidOperationException("inner");
            var ex = new BooseRuntimeException("runtime problem", inner);

            Assert.AreEqual("runtime problem", ex.Message);
            Assert.AreSame(inner, ex.InnerException);
        }
    }
}