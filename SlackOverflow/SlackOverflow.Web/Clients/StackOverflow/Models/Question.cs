﻿using System.Text.Json.Serialization;

namespace SlackOverflow.Web.Clients.StackOverflow.Models
{
    public record Question
    {
        [JsonPropertyName("question_id")]
        public int QuestionId { get; set; }

        public string Title { get; set; } = default!;
        public string? Body { get; set; }

        public IEnumerable<Answer> Answers { get; set; } = Enumerable.Empty<Answer>();

        public string[] Tags { get; set; } = default!;
        public Owner Owner { get; set; } = default!;

        [JsonPropertyName("is_answered")]
        public bool IsAnswered { get; set; }

        [JsonPropertyName("answer_count")]
        public int AnswerCount { get; set; }
        
        
    }


}
