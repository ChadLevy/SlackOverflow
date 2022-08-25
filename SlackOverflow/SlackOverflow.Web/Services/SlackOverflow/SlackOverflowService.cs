using SlackOverflow.Web.Clients.StackOverflow.Models;
using SlackOverflow.Web.Clients.StackOverflowClient;
using SlackOverflow.Web.Extensions;
using SlackOverflow.Web.ViewModels;

namespace SlackOverflow.Web.Services.SlackOverflow
{
    public class SlackOverflowService : ISlackOverflowService
    {
        private readonly IStackOverflowClient _stackOverflowClient;

        public SlackOverflowService(IStackOverflowClient stackOverflowClient)
        {
            _stackOverflowClient = stackOverflowClient;
        }

        public async Task<IEnumerable<Question>> GetQuestionsAsync()
        {
            return await GetQuestionsAsync(10);
        }

        public async Task<IEnumerable<Question>> GetQuestionsAsync(int limit)
        {
            var questions = await _stackOverflowClient.GetQuestionsAsync();
            var questionsWithAnswers = questions.Where(s => s.AnswerCount > 1 && s.HasAcceptedAnswer);

            return questionsWithAnswers.Take(limit) ?? Enumerable.Empty<Question>();
        }

        public async Task<Question> GetQuestionAsync(int questionId)
        {
            var question = await _stackOverflowClient.GetQuestionAsync(questionId);

            // Requirement 2: Select a question to view all the answers for that question *in a random order*.
            question.Answers = question.Answers.Shuffle();

            return question;
        }

        public async Task<AnswerResult> AnswerQuestion(int questionId, int answerId)
        {
            var question = await _stackOverflowClient.GetQuestionAsync(questionId);

            AnswerResult result = new AnswerResult()
            {
                CorrectAnswer = question.AcceptedAnswerId == answerId,
                Question = question,
                AcceptedAnswer = question.Answers.FirstOrDefault(a => a.AnswerId == question.AcceptedAnswerId),
            };

            if(!result.CorrectAnswer)
            {
                result.UserSelectedAnswer = question.Answers.FirstOrDefault(a => a.AnswerId == answerId);
            }

            return result;
        }
    }
}