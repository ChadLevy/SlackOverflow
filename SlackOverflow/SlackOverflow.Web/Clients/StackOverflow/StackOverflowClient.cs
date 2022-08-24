﻿using SlackOverflow.Web.Clients.StackOverflow.Models;
using RestSharp;
using static SlackOverflow.Web.Clients.StackOverflow.StackOverflowExceptions;

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

        private int quotaMax = 0;
        private int quotaRemaining = 0;
        private int backoff = 0;

        public StackOverflowClient(ILogger<StackOverflowClient> logger)
        {
            _client = new RestClient(_baseUrl);
            _logger = logger;
        }

        /// <summary>
        /// Get a list of recent questions.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Question>> GetQuestionsAsync()
        {
            _logger.LogInformation("GetQuestionsAsync");

            var response = await _client.ExecuteAsync<Response<Question>>(
                new RestRequest($"/questions?order=desc&sort=week&site=stackoverflow&filter={ _recentQuestionsFilter }"));

            quotaMax = response.Data?.QuotaMax ?? 0;
            quotaRemaining = response.Data?.QuotaRemaining ?? 0;
            backoff = response.Data?.Backoff ?? 0;

            _logger.LogInformation(response.StatusCode.ToString());

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

            if(!response.IsSuccessful)
            {
                throw new StackOverflowAPIException($"GetQuestionAsync returned not successful: { response.ErrorException.Message }");
            }

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