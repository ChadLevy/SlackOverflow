using SlackOverflow.Web.Clients.StackOverflow.Models;

namespace SlackOverflow.Web.Services.SlackOverflow
{
    public interface ISlackOverflowService
    {
        Task<Question> GetQuestionAsync();
    }
}