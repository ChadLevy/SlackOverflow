﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SlackOverflow.Web.Services.SlackOverflow;

namespace SlackOverflow.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly ISlackOverflowService _slackOverflowService;
        public QuestionsController(ISlackOverflowService slackOverflowService)
        {
            _slackOverflowService = slackOverflowService;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var question = await _slackOverflowService.GetQuestionAsync();
            return Ok(question);
        }
    }
}