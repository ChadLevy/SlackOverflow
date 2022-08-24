using Microsoft.AspNetCore.Mvc;
using SlackOverflow.Web.Clients.StackOverflowClient;
using SlackOverflow.Web.Models;
using SlackOverflow.Web.Services.SlackOverflow;
using System.Diagnostics;

namespace SlackOverflow.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISlackOverflowService _slackOverflowService;

        public HomeController(ILogger<HomeController> logger, ISlackOverflowService slackOverflowService)
        {
            _logger = logger;
            _slackOverflowService = slackOverflowService;
        }

        public async Task<IActionResult> Index()
        {
            var questions = await _slackOverflowService.GetQuestionsAsync();
            return View(questions);
        }

        public async Task<IActionResult> Question(int id)
        {
            var question = await _slackOverflowService.GetQuestionAsync(id);
            return View(question);
        }

        public async Task<IActionResult> Answer(int questionId, int answerId)
        {
            var result = await _slackOverflowService.AnswerQuestion(questionId, answerId);
            return View(result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}