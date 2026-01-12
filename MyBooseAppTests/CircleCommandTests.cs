using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyBooseAppFramework;
using MyBooseAppFramework.Factories;

namespace MyBooseAppTests
{
    [TestClass]
    public class CircleCommandTests
    {
        /// <summary>
        /// Verifies that the circle command adds a circle entry into the runtime Commands list.
        /// </summary>
        [TestMethod]
        public void Circle_AddsCircleCommandToCommandsList()
        {
            var runner = new BooseProgramRunner();
            var factory = new CommandFactory();

            factory.Create("moveto", new[] { "100", "120" }).Execute(runner);
            factory.Create("pencolour", new[] { "0", "0", "255" }).Execute(runner);

            int before = runner.Commands.Count;

            var cmd = factory.Create("circle", new[] { "20" });
            cmd.Execute(runner);

            Assert.AreEqual(before + 1, runner.Commands.Count);
            StringAssert.StartsWith(runner.Commands[runner.Commands.Count - 1].ToLowerInvariant(), "circle");
        }
    }
}