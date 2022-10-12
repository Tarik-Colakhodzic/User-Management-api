using System;

namespace Domain.Exceptions
{
    public class CoreExceptions : Exception
    {
        public CoreExceptions(string message) : base(message)
        {
        }

        public CoreExceptions(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}