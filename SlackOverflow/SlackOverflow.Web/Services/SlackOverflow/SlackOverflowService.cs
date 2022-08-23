using SlackOverflow.Web.Clients.StackOverflow.Models;
using SlackOverflow.Web.Clients.StackOverflowClient;
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

            if(!questions.Any())
            {
                // TODO: determine what to do if no questions are returned.
                return new Question();
            }

            var questionsWithAnswers = questions.Where(s => 
                s.AnswerCount > 1 
                && s.Answers.Any(a => a.IsAccepted == true));

            if (!questionsWithAnswers.Any())
            {
                /* TODO: determine what to do if no questions with answers are returned.
                 * Ideas:
                 *  1. Rerun the query. Perhaps put the whole thing in a loop and rerun the query until we get a question with an answer.
                 *  2. Throw an error and let the user know that there are no questions with answers.
                 */
                return new Question();
            }

            var randomQuestion = questionsWithAnswers.GetRandom();

            randomQuestion.Answers = randomQuestion.Answers.Shuffle();

            return randomQuestion;
        }
    }
}