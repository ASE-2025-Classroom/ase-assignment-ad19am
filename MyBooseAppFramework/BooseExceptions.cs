using System;

namespace MyBooseAppFramework
{
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