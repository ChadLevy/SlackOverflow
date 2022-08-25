namespace SlackOverflow.Web.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public string? LastErrorName { get; set; }
        public string? LastErrorMessage { get; set; }
    }
}