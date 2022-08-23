using System.Text.Json.Serialization;

namespace SlackOverflow.Web.Clients.StackOverflow.Models
{
    public record Owner
    {
        [JsonPropertyName("display_name")]
        public string DisplayName { get; set; } = default!;

        [JsonPropertyName("reputation")]
        public int Reputation { get; set; }

        [JsonPropertyName("profile_image")]
        public string ProfileImage { get; set; } = default!;
    }
}