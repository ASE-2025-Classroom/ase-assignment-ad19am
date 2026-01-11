using System.Collections.Generic;
using System.Drawing;

namespace MyBooseAppFramework.Interfaces
{
    public interface IBooseRuntime
    {
        BoosePen Pen { get; }
        Color PenColour { get; set; }
        List<string> Commands { get; }
    }
}
