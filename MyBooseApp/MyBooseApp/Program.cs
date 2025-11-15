using System;
using MyBooseAppFramework;

namespace MyBooseApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var runner = new BooseProgramRunner();

            string program = @"moveto 10,10
drawto 20,20
moveto 5,5";

            try
            {
                runner.Run(program);
                Console.WriteLine($"Pen is now at ({runner.Pen.X}, {runner.Pen.Y})");
            }
            catch (BooseSyntaxException ex)
            {
                Console.WriteLine("Syntax error in BOOSE program:");
                Console.WriteLine(ex.Message);
            }
            catch (BooseRuntimeException ex)
            {
                Console.WriteLine("Runtime error while executing BOOSE program:");
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error:");
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();
        }
    }
}