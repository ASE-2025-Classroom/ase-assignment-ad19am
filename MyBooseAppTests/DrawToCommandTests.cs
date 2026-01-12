using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyBooseAppFramework;
using MyBooseAppFramework.Factories;

namespace MyBooseAppTests
{
    [TestClass]
    public class DrawToCommandTests
    {
        /// <summary>
        /// Verifies that the drawto command updates the pen position in the runtime.
        /// </summary>
        [TestMethod]
        public void DrawTo_UpdatesPenPosition()
        {
            var runner = new BooseProgramRunner();
            var factory = new CommandFactory();

            factory.Create("moveto", new[] { "10", "10" }).Execute(runner);

            var cmd = factory.Create("drawto", new[] { "50", "75" });
            cmd.Execute(runner);

            Assert.AreEqual(50, runner.Pen.X);
            Assert.AreEqual(75, runner.Pen.Y);
        }
    }
}