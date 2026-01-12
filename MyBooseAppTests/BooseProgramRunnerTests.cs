using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyBooseAppFramework;

namespace MyBooseAppTests
{
    /// <summary>
    /// Unit tests for BooseProgramRunner.
    /// </summary>
    [TestClass]
    public class BooseProgramRunnerTests
    {
        /// <summary>
        /// Verifies that a multi-line program leaves the pen at the final moveto position.
        /// </summary>
        [TestMethod]
        public void Run_MultilineProgram_LeavesPenAtLastPosition()
        {
            var runner = new BooseProgramRunner();

            string program = @"moveto 10,10
drawto 20,20
moveto 5,5";

            runner.Run(program);

            Assert.AreEqual(5, runner.Pen.X);
            Assert.AreEqual(5, runner.Pen.Y);
        }

        /// <summary>
        /// Verifies that 'circle' generates a command using the current pen position.
        /// </summary>
        [TestMethod]
        public void Run_Circle_AddsCircleCommand()
        {
            var runner = new BooseProgramRunner();

            string program = @"moveto 10,20
circle 30";

            runner.Run(program);

            bool foundCircle = runner.Commands.Exists(c => c.StartsWith("circle "));
            Assert.IsTrue(foundCircle, "Expected a circle command to be generated.");
        }

        /// <summary>
        /// Verifies that 'pencolour' changes the pen colour used in later drawing commands.
        /// </summary>
        [TestMethod]
        public void Run_PenColour_AffectsSubsequentCommands()
        {
            var runner = new BooseProgramRunner();

            string program = @"pencolour 1,2,3
moveto 5,5
circle 10";

            runner.Run(program);

            string circle = runner.Commands.Find(c => c.StartsWith("circle"));
            Assert.IsNotNull(circle, "Expected a circle command.");

            Assert.IsTrue(circle.Contains(",1,2,3"), "Expected circle command to include pencolour RGB 1,2,3.");
        }
    }
}