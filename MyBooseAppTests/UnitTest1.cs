using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyBooseAppFramework;

namespace MyBooseAppTests
{
    /// <summary>
    /// Unit tests for BoosePen.
    /// </summary>
    [TestClass]
    public class BoosePenTests
    {
        /// <summary>
        /// Verifies that MoveTo updates the X and Y coordinates without drawing.
        /// </summary>
        [TestMethod]
        public void MoveTo_UpdatesPenPosition()
        {
            var pen = new BoosePen();

            pen.MoveTo(100, 200);

            Assert.AreEqual(100, pen.X);
            Assert.AreEqual(200, pen.Y);
        }

        /// <summary>
        /// Verifies that DrawTo updates the X and Y coordinates.
        /// </summary>
        [TestMethod]
        public void DrawTo_UpdatesPenPosition()
        {
            var pen = new BoosePen();
            pen.MoveTo(0, 0);

            pen.DrawTo(50, 75);

            Assert.AreEqual(50, pen.X);
            Assert.AreEqual(75, pen.Y);
        }
    }
}