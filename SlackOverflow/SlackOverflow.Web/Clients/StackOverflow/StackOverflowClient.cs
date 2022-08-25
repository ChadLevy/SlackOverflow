using SlackOverflow.Web.Clients.StackOverflow.Models;
using SlackOverflow.Web;
using RestSharp;
using static SlackOverflow.Web.Clients.StackOverflow.StackOverflowExceptions;
using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;

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

        private readonly IMemoryCache _cache;
        private readonly ILogger<StackOverflowClient> _logger;
        private readonly RestClient _client;

        private int _quotaMax = 0;
        private int _quotaRemaining = 0;
        private int _backoff = 0;
        private DateTime _nextAPICall = DateTime.UtcNow;

        public StackOverflowClient(ILogger<StackOverflowClient> logger, IMemoryCache cache)
        {
            _client = new RestClient(_baseUrl);
            _logger = logger;
            _cache = cache;
        }

        /// <summary>
        /// Get a list of recent questions.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Question>> GetQuestionsAsync()
        {
            var response = await APICall($"/questions?order=desc&sort=week&site=stackoverflow&filter={_recentQuestionsFilter}");
            return response;
        }
            

        /// <summary>
        /// Gets a given question and its answers.
        /// </summary>
        /// <param name="questionId">StackOverflow question ID</param>
        /// <returns></returns>
        public async Task<QuestionWithAnswers> GetQuestionAsync(int questionId)
        {
            var response = await APICall($"/questions/{questionId}?order=desc&sort=activity&site=stackoverflow&filter={_questonWithAnswerFilter}");
            return response.FirstOrDefault();
        }
        
        /// <summary>
        /// Wrapper method for all SO API calls.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resource"></param>
        /// <returns></returns>
        /// <exception cref="StackOverflowThrottleViolationException"></exception>
        /// <exception cref="StackOverflowInternalErrorException"></exception>
        /// <exception cref="StackOverflowUnavailableException"></exception>
        private async Task<IEnumerable<QuestionWithAnswers>> APICall(string resource)
        {
            // If the last API call returned with a backoff value,
            // ensure we wait until we're allowed to call the API again.
            if(_backoff > 0)
            {
                var waitTime = DateTime.UtcNow - _nextAPICall;
                _logger.LogWarning("StackOverflowClient: waiting {waitTime} seconds before call.", waitTime);
                await Task.Delay(waitTime);
            }

            var response = await _client.ExecuteAsync<Response<QuestionWithAnswers>>(new RestRequest(resource));

            _quotaMax = response.Data?.QuotaMax ?? _quotaMax;
            _quotaRemaining = response.Data?.QuotaRemaining ?? _quotaRemaining;
            _backoff = response.Data?.Backoff ?? 0;

            // If the response returned with a backoff value,
            // store it and the next allowed API call time.
            if (response.Data?.Backoff != null)
            {
                _logger.LogWarning("StackOverflowClient: backoff field returned with value {_backoff}", _backoff);
                _nextAPICall = DateTime.UtcNow.AddSeconds(_backoff);
            }

            if(response.StatusCode == HttpStatusCode.BadRequest)
            {
                _ = response.Data?.ErrorName switch
                {
                    "throttle_violation" => throw new StackOverflowThrottleViolationException(response.Data?.ErrorMessage),
                    "internal_error" => throw new StackOverflowInternalErrorException(response.Data?.ErrorMessage),
                    "temporarily_unavailable" => throw new StackOverflowUnavailableException(response.Data?.ErrorMessage),
                    _ => ""
                };
            }

            _cache.Set<int>(Constants.StackOverflowAPI.QuotaMaxKeyName, _quotaMax);
            _cache.Set<int>(Constants.StackOverflowAPI.QuotaRemainingKeyName, _quotaRemaining);
            _cache.Set<int>(Constants.StackOverflowAPI.BackoffKeyName, _backoff);

            return response.Data?.Items;
        }

        public void Dispose()
        {
            _client?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}