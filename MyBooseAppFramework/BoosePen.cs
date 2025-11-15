namespace MyBooseAppFramework
{
    /// <summary>
    /// Represents the virtual drawing pen used by the BOOSE interpreter.
    /// Tracks its current X and Y coordinates.
    /// </summary>
    public class BoosePen
    {
        /// <summary>
        /// Current X coordinate of the pen.
        /// </summary>
        public int X { get; private set; }

        /// <summary>
        /// Current Y coordinate of the pen.
        /// </summary>
        public int Y { get; private set; }

        /// <summary>
        /// Creates a new pen at position (0,0).
        /// </summary>
        public BoosePen()
        {
            X = 0;
            Y = 0;
        }

        /// <summary>
        /// Moves the pen to the specified position without drawing.
        /// </summary>
        /// <param name="x">X coordinate to move to.</param>
        /// <param name="y">Y coordinate to move to.</param>
        public void MoveTo(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Draws to the specified position (currently just updates X and Y).
        /// </summary>
        /// <param name="x">X coordinate to draw to.</param>
        /// <param name="y">Y coordinate to draw to.</param>
        public void DrawTo(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}