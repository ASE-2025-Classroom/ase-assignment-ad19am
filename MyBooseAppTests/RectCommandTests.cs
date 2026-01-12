using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyBooseAppFramework;
using MyBooseAppFramework.Factories;

namespace MyBooseAppTests
{
    [TestClass]
    public class RectCommandTests
    {
        /// <summary>
        /// Verifies that the rect command adds a rectangle entry into the runtime Commands list.
        /// </summary>
        [TestMethod]
        public void Rect_AddsRectCommandToCommandsList()
        {
            var runner = new BooseProgramRunner();
            var factory = new CommandFactory();

            factory.Create("moveto", new[] { "20", "30" }).Execute(runner);
            factory.Create("pencolour", new[] { "10", "20", "30" }).Execute(runner);

            int before = runner.Commands.Count;

            var cmd = factory.Create("rect", new[] { "40", "50" });
            cmd.Execute(runner);

            Assert.AreEqual(before + 1, runner.Commands.Count);
            StringAssert.StartsWith(runner.Commands[runner.Commands.Count - 1].ToLowerInvariant(), "rect");
        }
    }
}