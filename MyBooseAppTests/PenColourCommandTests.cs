using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyBooseAppFramework;
using MyBooseAppFramework.Factories;
using System.Drawing;

namespace MyBooseAppTests
{
    /// <summary>
    /// Tests for the PenColour command.
    /// Verifies that RGB values are correctly applied to the runtime.
    /// </summary>
    [TestClass]
    public class PenColourCommandTests
    {
        [TestMethod]
        public void PenColourCommand_UpdatesPenColorRGB()
        {
            var runner = new BooseProgramRunner();
            var factory = new CommandFactory();

            factory.Create("pencolour", new[] { "10", "20", "30" })
                   .Execute(runner);

            Assert.AreEqual(10, runner.PenColor.R);
            Assert.AreEqual(20, runner.PenColor.G);
            Assert.AreEqual(30, runner.PenColor.B);
        }
    }
}