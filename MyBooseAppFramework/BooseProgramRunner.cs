using System;
using System.Collections.Generic;
using System.Drawing;

namespace MyBooseAppFramework
{
    /// <summary>
    /// Executes simple BOOSE programs consisting of moveto, drawto and
    /// basic drawing commands. Maintains a single BoosePen instance.
    /// </summary>
    public class BooseProgramRunner
    {
        /// <summary>
        /// Returns information about this BOOSE library, used for the About display.
        /// </summary>
        public static string About()
        {
            return "MyBooseAppFramework - simple BOOSE interpreter for ASE assignment.";
        }

        /// <summary>
        /// The pen used by this runner to track the current coordinates.
        /// </summary>
        public BoosePen Pen { get; }

        /// <summary>
        /// Current pen colour.
        /// </summary>
        public Color PenColor { get; private set; } = Color.Black;

        /// <summary>
        /// List of drawing commands produced while running the program.
        /// Each entry is a simple string that the UI can parse and draw.
        /// </summary>
        public List<string> Commands { get; } = new List<string>();

        /// <summary>
        /// Creates a new program runner with a fresh pen at (0,0).
        /// </summary>
        public BooseProgramRunner()
        {
            Pen = new BoosePen();
            PenColor = Color.Black;
        }

        /// <summary>
        /// Runs a BOOSE program made up of commands (one per line).
        /// Supports: moveto, drawto, pencolour, circle, rect, write.
        /// </summary>
        /// <param name="programText">
        /// The text of the program. Lines such as 'moveto 10,10' or 'circle 50'.
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown if programText is null.</exception>
        /// <exception cref="BooseSyntaxException">
        /// Thrown when the program contains unknown commands or badly formatted coordinates.
        /// </exception>
        /// <exception cref="BooseRuntimeException">
        /// Thrown when an unexpected runtime error occurs while executing the program.
        /// </exception>
        public void Run(string programText)
        {
            if (programText == null)
                throw new ArgumentNullException(nameof(programText));

            Commands.Clear();

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

                        Commands.Add($"drawto {x},{y},{PenColor.R},{PenColor.G},{PenColor.B}");
                    }
                    else if (lower.StartsWith("pencolour"))
                    {
                        var args = line.Substring("pencolour".Length).Trim().Split(',');
                        if (args.Length != 3)
                            throw new FormatException("pencolour requires three comma-separated numbers (r,g,b).");

                        int r = int.Parse(args[0]);
                        int g = int.Parse(args[1]);
                        int b = int.Parse(args[2]);

                        PenColor = Color.FromArgb(r, g, b);
                        Commands.Add($"pencolour {r},{g},{b}");
                    }
                    else if (lower.StartsWith("circle"))
                    {
                        string arg = line.Substring("circle".Length).Trim();
                        int radius = int.Parse(arg);

                        Commands.Add(
                            $"circle {Pen.X},{Pen.Y},{radius},{PenColor.R},{PenColor.G},{PenColor.B}");
                    }
                    else if (lower.StartsWith("rect"))
                    {
                        var args = line.Substring("rect".Length).Trim().Split(',');
                        if (args.Length != 2)
                            throw new FormatException("rect requires two comma-separated numbers (width,height).");

                        int w = int.Parse(args[0]);
                        int h = int.Parse(args[1]);

                        Commands.Add(
                            $"rect {Pen.X},{Pen.Y},{w},{h},{PenColor.R},{PenColor.G},{PenColor.B}");
                    }
                    else if (lower.StartsWith("write"))
                    {
                        string text = line.Substring("write".Length).Trim();
                        text = text.Trim('"');

                        Commands.Add(
                            $"write {Pen.X},{Pen.Y},\"{text}\",{PenColor.R},{PenColor.G},{PenColor.B}");
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

        /// <summary>
        /// Parses two integer coordinates from a command line, e.g. 'moveto 100,200'.
        /// </summary>
        /// <param name="line">The full line of text from the program.</param>
        /// <param name="command">The command name (moveto or drawto).</param>
        /// <param name="lineNumber">The current line number for error reporting.</param>
        /// <returns>A tuple containing the parsed X and Y coordinates.</returns>
        /// <exception cref="FormatException">
        /// Thrown when the coordinates cannot be parsed as two comma-separated integers.
        /// </exception>
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