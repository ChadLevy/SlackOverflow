using SlackOverflow.Web.Clients.StackOverflowClient.Models;

namespace SlackOverflow.Web.Clients.StackOverflowClient
{
    public interface IStackOverflowClient
    {
        Task<IEnumerable<Question>> GetQuestionsAsync();
    }
}