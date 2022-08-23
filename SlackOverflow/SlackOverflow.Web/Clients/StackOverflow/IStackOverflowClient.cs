using SlackOverflow.Web.Clients.StackOverflow.Models;

namespace SlackOverflow.Web.Clients.StackOverflowClient
{
    public interface IStackOverflowClient
    {
        Task<IEnumerable<Question>> GetQuestionsAsync();
    }
}