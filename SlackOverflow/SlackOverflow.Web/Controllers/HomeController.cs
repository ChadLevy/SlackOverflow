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

        public async Task<IActionResult> GetQuestion()
        {
            var question = await _slackOverflowService.GetQuestionAsync();
            return Json(question);
        }
        

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}