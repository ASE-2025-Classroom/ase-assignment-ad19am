using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyBooseAppFramework.Factories;
using MyBooseAppFramework.Interfaces;
using System;

namespace MyBooseAppTests
{
    /// <summary>
    /// Unit tests for CommandFactory.
    /// </summary>
    [TestClass]
    public class CommandFactoryTests
    {
        /// <summary>
        /// Verifies factory returns a valid command for a known keyword.
        /// </summary>
        [TestMethod]
        public void Create_MoveTo_ReturnsCommand()
        {
            ICommandFactory factory = new CommandFactory();

            ICommand cmd = factory.Create("moveto", new[] { "10", "20" });

            Assert.IsNotNull(cmd);
            Assert.AreEqual("moveto", cmd.Name);
        }

        /// <summary>
        /// Verifies factory throws for an unknown keyword.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_UnknownCommand_ThrowsArgumentException()
        {
            var factory = new CommandFactory();
            factory.Create("notARealCommand", new string[0]);
        }
    }
}