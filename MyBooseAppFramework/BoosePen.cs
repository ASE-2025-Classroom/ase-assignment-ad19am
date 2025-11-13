namespace MyBooseAppFramework
{

    public class BoosePen
    {
        public int X { get; private set; }

        public int Y { get; private set; }

        public BoosePen()
        {
            X = 0;
            Y = 0;
        }

        public void MoveTo(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void DrawTo(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
