using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyBooseAppFramework;
using MyBooseAppFramework.Factories;

namespace MyBooseAppTests
{
    [TestClass]
    public class MoveToCommandTests
    {
        /// <summary>
        /// Verifies that the moveto command updates the pen position in the runtime.
        /// </summary>
        [TestMethod]
        public void MoveTo_UpdatesPenPosition()
        {
            var runner = new BooseProgramRunner();
            var factory = new CommandFactory();

            var cmd = factory.Create("moveto", new[] { "100", "200" });
            cmd.Execute(runner);

            Assert.AreEqual(100, runner.Pen.X);
            Assert.AreEqual(200, runner.Pen.Y);
        }
    }
}