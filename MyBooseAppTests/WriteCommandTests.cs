using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyBooseAppFramework;
using MyBooseAppFramework.Factories;

namespace MyBooseAppTests
{
    [TestClass]
    public class WriteCommandTests
    {
        /// <summary>
        /// Verifies that the write command adds a write entry into the runtime Commands list.
        /// </summary>
        [TestMethod]
        public void Write_AddsWriteCommandToCommandsList()
        {
            var runner = new BooseProgramRunner();
            var factory = new CommandFactory();

            factory.Create("moveto", new[] { "10", "10" }).Execute(runner);
            factory.Create("pencolour", new[] { "0", "0", "255" }).Execute(runner);

            int before = runner.Commands.Count;

            var cmd = factory.Create("write", new[] { "\"hello\"" });
            cmd.Execute(runner);

            Assert.AreEqual(before + 1, runner.Commands.Count);
            StringAssert.StartsWith(runner.Commands[runner.Commands.Count - 1].ToLowerInvariant(), "write");
        }
    }
}