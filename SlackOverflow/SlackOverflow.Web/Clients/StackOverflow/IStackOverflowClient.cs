using SlackOverflow.Web.Clients.StackOverflow.Models;

namespace SlackOverflow.Web.Clients.StackOverflowClient
{
    public interface IStackOverflowClient
    {
        Task<QuestionWithAnswers> GetQuestionAsync(int questionId);
        Task<IEnumerable<Question>> GetQuestionsAsync();
    }
}