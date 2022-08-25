using System.Runtime.Serialization;

namespace SlackOverflow.Web.Clients.StackOverflow
{
    public class StackOverflowExceptions
    {

        public class StackOverflowAPIException : Exception
        {
            public StackOverflowAPIException(){ }
            public StackOverflowAPIException(string? message) : base(message) { }

            public StackOverflowAPIException(string? message, Exception? innerException) : base(message, innerException) { }

        }

        public class StackOverflowThrottleViolationException : StackOverflowAPIException
        {
            public StackOverflowThrottleViolationException(string? message) : base(message) { }
        }

        public class StackOverflowInternalErrorException : StackOverflowAPIException
        {
            public StackOverflowInternalErrorException(string? message) : base(message) { }
        }

        public class StackOverflowUnavailableException : StackOverflowAPIException
        {
            public StackOverflowUnavailableException(string? message) : base(message) { }
        }

    }
}
