using System;
using MyBooseAppFramework;

namespace MyBooseApp
{
    internal class Program
    {
        [STAThread]
        static void Main()
        {
            var runner = new BooseProgramRunner();

            string program = @"moveto 10,10
drawto 20,20
moveto 5,5";

            runner.Run(program);

            Console.WriteLine($"Pen is now at ({runner.Pen.X}, {runner.Pen.Y})");
            Console.ReadLine();
        }
    }
}
