﻿using SlackOverflow.Web.Clients.StackOverflow.Models;
using RestSharp;
using static SlackOverflow.Web.Clients.StackOverflow.StackOverflowExceptions;
using System.Net;

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

        private readonly ILogger<StackOverflowClient> _logger;
        private readonly RestClient _client;

        private int _quotaMax = 0;
        private int _quotaRemaining = 0;
        private int _backoff = 0;
        private DateTime _nextAPICall = DateTime.UtcNow;

        public StackOverflowClient(ILogger<StackOverflowClient> logger)
        {
            _client = new RestClient(_baseUrl);
            _logger = logger;
        }

        /// <summary>
        /// Get a list of recent questions.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Question>> GetQuestionsAsync() => 
            await APICall<IEnumerable<Question>>($"/questions?order=desc&sort=week&site=stackoverflow&filter={_recentQuestionsFilter}");

        /// <summary>
        /// Gets a given question and its answers.
        /// </summary>
        /// <param name="questionId">StackOverflow question ID</param>
        /// <returns></returns>
        public async Task<QuestionWithAnswers> GetQuestionAsync(int questionId) => 
            await APICall<QuestionWithAnswers>($"/questions/{questionId}?order=desc&sort=activity&site=stackoverflow&filter={_questonWithAnswerFilter}");

        /// <summary>
        /// Wrapper method for all SO API calls.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resource"></param>
        /// <returns></returns>
        /// <exception cref="StackOverflowThrottleViolationException"></exception>
        /// <exception cref="StackOverflowInternalErrorException"></exception>
        /// <exception cref="StackOverflowUnavailableException"></exception>
        private async Task<T> APICall<T>(string resource) where T: class
        {
            // If the last API call returned with a backoff value,
            // ensure we wait until we're allowed to call the API again.
            if(_backoff > 0)
            {
                var waitTime = DateTime.UtcNow - _nextAPICall;
                _logger.LogWarning("StackOverflowClient: waiting {waitTime} seconds before call.", waitTime);
                await Task.Delay(waitTime);
            }

            var response = await _client.ExecuteAsync<Response<T>>(new RestRequest(resource));

            _quotaMax = response.Data?.QuotaMax ?? _quotaMax;
            _quotaRemaining = response.Data?.QuotaRemaining ?? _quotaRemaining;
            _backoff = response.Data?.Backoff ?? _backoff;

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

            return response.Data as T ?? default!;
        }

        public void Dispose()
        {
            _client?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}