using SlackOverflow.Web.Clients.StackOverflowClient.Models;

namespace SlackOverflow.Web.Services.SlackOverflow
{
    public interface ISlackOverflowService
    {
        Task<Question> GetQuestionAsync();
    }
}