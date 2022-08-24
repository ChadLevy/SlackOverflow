using SlackOverflow.Web.Clients.StackOverflow.Models;
using SlackOverflow.Web.ViewModels;

namespace SlackOverflow.Web.Services.SlackOverflow
{
    public interface ISlackOverflowService
    {
        Task<Question> GetQuestionAsync(int id);
        Task<IEnumerable<Question>> GetQuestionsAsync();
        Task<IEnumerable<Question>> GetQuestionsAsync(int limit);
        Task<AnswerResult> AnswerQuestion(int questionId, int answerId);
    }
}