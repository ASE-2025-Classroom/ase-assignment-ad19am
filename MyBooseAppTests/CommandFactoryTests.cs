using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyBooseAppFramework.Factories;
using MyBooseAppFramework.Interfaces;

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
        [ExpectedException(typeof(System.Exception))]
        public void Create_UnknownCommand_Throws()
        {
            ICommandFactory factory = new CommandFactory();
            factory.Create("doesnotexist", new string[0]);
        }
    }
}