using SlackOverflow.Web.Clients.StackOverflow.Models;
using RestSharp;

namespace SlackOverflow.Web.Clients.StackOverflowClient
{
    public class StackOverflowClient : IStackOverflowClient, IDisposable
    {
        private const string _baseUrl = "https://api.stackexchange.com/2.3";

        private const string _recentQuestionsFilter = "!17m3FOC5n)fwRg-07LfOgIT6NVZEQuKCoQg)8OO(97f0AU";

        /// <summary>
        /// Filter for getting a question with its answers.
        /// </summary>
        private const string _questonWithAnswerFilter = "!lzzdvhZk)kraeGP)KuAGXxyK)2lhNqi5kbPVfsfdte6-qaf4pi3K6R)D";

        private readonly RestClient _client;
        
        public StackOverflowClient()
        {
            _client = new RestClient(_baseUrl);
        }

        /// <summary>
        /// Get a list of recent questions.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Question>> GetQuestionsAsync()
        {
            var response = await _client.ExecuteAsync<Response<Question>>(
                new RestRequest($"/questions?order=desc&sort=week&site=stackoverflow&filter={ _recentQuestionsFilter }"));

            var items = response!.Data?.Items;

            return items?.ToList() ?? Enumerable.Empty<Question>();
        }

        /// <summary>
        /// Gets a given question and its answers.
        /// </summary>
        /// <param name="questionId">StackOverflow question ID</param>
        /// <returns></returns>
        public async Task<QuestionWithAnswers> GetQuestionAsync(int questionId)
        {
            var response = await _client.ExecuteAsync<Response<QuestionWithAnswers>>(
                new RestRequest($"/questions/{questionId}?order=desc&sort=activity&site=stackoverflow&filter={ _questonWithAnswerFilter }"));

            var question = response!.Data?.Items.FirstOrDefault();

            return question;
        }

        public void Dispose()
        {
            _client?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}