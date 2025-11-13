using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyBooseAppFramework;

namespace MyBooseAppTests
{
    [TestClass]
    public class BoosePenTests
    {
        [TestMethod]
        public void MoveTo_UpdatesPenPosition()
        {
            var pen = new BoosePen();

            pen.MoveTo(100, 200);

            Assert.AreEqual(100, pen.X);
            Assert.AreEqual(200, pen.Y);
        }

        [TestMethod]
        public void DrawTo_UpdatesPenPosition()
        {
            var pen = new BoosePen();
            pen.MoveTo(0, 0);

            pen.DrawTo(50, 75);

            Assert.AreEqual(50, pen.X);
            Assert.AreEqual(75, pen.Y);
        }

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
    }
}