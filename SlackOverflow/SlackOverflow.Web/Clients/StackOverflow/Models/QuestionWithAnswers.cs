namespace SlackOverflow.Web.Clients.StackOverflow.Models
{
    public record QuestionWithAnswers : Question
    {
        public IEnumerable<Answer> Answers { get; set; } = Enumerable.Empty<Answer>();
    }
}
