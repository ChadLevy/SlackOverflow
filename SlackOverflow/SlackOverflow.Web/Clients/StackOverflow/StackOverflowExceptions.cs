namespace SlackOverflow.Web.Clients.StackOverflow
{
    public class StackOverflowExceptions
    {

        public class StackOverflowAPIException : Exception
        {
            public StackOverflowAPIException(string? message) : base(message)
            {

            }
        }

    }
}
