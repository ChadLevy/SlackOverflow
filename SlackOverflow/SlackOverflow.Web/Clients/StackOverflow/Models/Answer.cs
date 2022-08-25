using System.Text.Json.Serialization;

namespace SlackOverflow.Web.Clients.StackOverflow.Models
{
    public record Answer
    {
        [JsonPropertyName("answer_id")]
        public int AnswerId { get; set; }
        public int Score { get; set; }
        public string Title { get; set; } = default!;
        public string Body { get; set; } = default!;
        public Owner Owner { get; set; } = default!;

        [JsonPropertyName("is_accepted")]
        public bool IsAccepted { get; set; }
    }
}
