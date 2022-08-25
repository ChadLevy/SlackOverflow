namespace SlackOverflow.Web.Clients.StackOverflow.Models
{
    public class QuestionWithAnswers : Question
    {
        public IEnumerable<Answer> Answers { get; set; } = Enumerable.Empty<Answer>();
    }
}
