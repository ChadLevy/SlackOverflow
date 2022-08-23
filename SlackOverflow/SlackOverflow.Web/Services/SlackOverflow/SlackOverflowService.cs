using SlackOverflow.Web.Clients.StackOverflowClient;
using SlackOverflow.Web.Clients.StackOverflowClient.Models;
using SlackOverflow.Web.Extensions;

namespace SlackOverflow.Web.Services.SlackOverflow
{
    public class SlackOverflowService : ISlackOverflowService
    {
        private readonly IStackOverflowClient _stackOverflowClient;

        public SlackOverflowService(IStackOverflowClient stackOverflowClient)
        {
            _stackOverflowClient = stackOverflowClient;
        }

        public async Task<Question> GetQuestionAsync()
        {
            var questions = (await _stackOverflowClient.GetQuestionsAsync()).ToList();

            var questionsWithAnswers = questions.Where(s => s.AnswerCount > 1 && s.IsAnswered);

            var randomQuestion = questionsWithAnswers.GetRandom();

            return randomQuestion;
        }
    }
}