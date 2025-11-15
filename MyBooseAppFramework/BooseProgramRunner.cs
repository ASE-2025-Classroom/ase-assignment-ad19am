using System;

namespace MyBooseAppFramework
{
    public class BooseProgramRunner
    {

        public BoosePen Pen { get; }

        public BooseProgramRunner()
        {
            Pen = new BoosePen();
        }

        public void Run(string programText)
        {
            if (programText == null)
                throw new ArgumentNullException(nameof(programText));

            var lines = programText.Split(
                new[] { "\r\n", "\n" },
                StringSplitOptions.RemoveEmptyEntries);

            int lineNumber = 0;

            foreach (var rawLine in lines)
            {
                lineNumber++;
                string line = rawLine.Trim();

                if (string.IsNullOrWhiteSpace(line))
                    continue;

                string lower = line.ToLowerInvariant();

                try
                {
                    if (lower.StartsWith("moveto"))
                    {
                        var (x, y) = ParseTwoInts(line, "moveto", lineNumber);
                        Pen.MoveTo(x, y);
                    }
                    else if (lower.StartsWith("drawto"))
                    {
                        var (x, y) = ParseTwoInts(line, "drawto", lineNumber);
                        Pen.DrawTo(x, y);
                    }
                    else
                    {
                        throw new BooseSyntaxException(
                            $"Line {lineNumber}: Unknown command '{line}'.");
                    }
                }
                catch (FormatException ex)
                {
                    throw new BooseSyntaxException(
                        $"Line {lineNumber}: {ex.Message}", ex);
                }
                catch (BooseSyntaxException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    throw new BooseRuntimeException(
                        $"Runtime error on line {lineNumber}: {ex.Message}", ex);
                }
            }
        }

        private static (int x, int y) ParseTwoInts(string line, string command, int lineNumber)
        {
            string argsPart = line.Substring(command.Length).Trim();

            var parts = argsPart.Split(',');

            if (parts.Length != 2)
            {
                throw new FormatException(
                    $"Line {lineNumber}: '{command}' requires two comma-separated numbers.");
            }

            if (!int.TryParse(parts[0].Trim(), out int x))
            {
                throw new FormatException(
                    $"Line {lineNumber}: Could not parse X coordinate in '{line}'.");
            }

            if (!int.TryParse(parts[1].Trim(), out int y))
            {
                throw new FormatException(
                    $"Line {lineNumber}: Could not parse Y coordinate in '{line}'.");
            }

            return (x, y);
        }
    }
}