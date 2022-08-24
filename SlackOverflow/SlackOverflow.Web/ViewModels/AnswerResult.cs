using SlackOverflow.Web.Clients.StackOverflow.Models;

namespace SlackOverflow.Web.ViewModels
{
    public record AnswerResult
    {
        public Question Question { get; set; } = default!;

        /// <summary>
        /// The accepted answer as specified by the question owner.
        /// </summary>
        public Answer AcceptedAnswer { get; set; } = default!;

        /// <summary>
        /// The answer the user chose. Is null if they got the answer right.
        /// </summary>
        public Answer? UserSelectedAnswer { get; set; }

        /// <summary>
        /// Indicates whether the user chose the accepted answer.
        /// </summary>
        public bool CorrectAnswer { get; set; }
    }
}