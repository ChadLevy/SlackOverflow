using SlackOverflow.Web.Clients.StackOverflow.Models;
using RestSharp;

namespace SlackOverflow.Web.Clients.StackOverflowClient
{
    public class StackOverflowClient : IStackOverflowClient, IDisposable
    {
        private readonly RestClient _client;
        
        public StackOverflowClient()
        {
            _client = new RestClient(Constants.StackOverflow.BaseUrl);
        }

        public async Task<IEnumerable<Question>> GetQuestionsAsync()
        {
            var response = await _client.ExecuteAsync<Response<Question>>(
                new RestRequest(Constants.StackOverflow.PresetRequests.RecentQuestions));

            var items = response!.Data?.Items;

            return items?.ToList() ?? Enumerable.Empty<Question>();
        }

        public void Dispose()
        {
            _client?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}