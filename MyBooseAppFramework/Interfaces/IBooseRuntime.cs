using System.Collections.Generic;
using System.Drawing;

namespace MyBooseAppFramework.Interfaces
{
    public interface IBooseRuntime
    {
        BoosePen Pen { get; }
        Colour PenColor { get; set; }
        List<string> Commands { get; }
    }
}
