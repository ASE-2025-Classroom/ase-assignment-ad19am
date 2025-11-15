using System;

namespace MyBooseAppFramework
{
    /// <summary>
    /// Represents an error in the syntax of a BOOSE program
    /// (e.g. unknown command or badly formatted coordinates).
    /// </summary>
    public class BooseSyntaxException : Exception
    {
        public BooseSyntaxException(string message)
            : base(message)
        {
        }

        public BooseSyntaxException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
    /// <summary>
    /// Represents a runtime error that occurs while executing a valid BOOSE program.
    /// </summary>
    public class BooseRuntimeException : Exception
    {
        public BooseRuntimeException(string message)
            : base(message)
        {
        }

        public BooseRuntimeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}