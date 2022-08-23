using System.Text.Json.Serialization;

namespace SlackOverflow.Web.Clients.StackOverflowClient.Models
{
    /// <summary>
    /// Wrapper object for all Stack Overflow API responses.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public record Response<T>
    {
        public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();
        public int Page { get; set; }
        
        [JsonPropertyName("page_size")]
        public int PageSize { get; set; }
        
        [JsonPropertyName("has_more")]
        public bool HasMore { get; set; }

        /// <summary>
        /// Indicates that subsequent API calls should not happen until Backoff number of seconds have passed.
        /// </summary>
        public int? Backoff { get; set; }
        
        [JsonPropertyName("quota_max")]
        public int QuotaMax { get; set; }
        
        [JsonPropertyName("quota_remaining")]
        public int QuotaRemaining { get; set; }

        [JsonPropertyName("error_id")]
        public int? ErrorId { get; set; }
        
        [JsonPropertyName("error_name")]
        public string? ErrorName { get; set; }
        
        [JsonPropertyName("error_message")]
        public string? ErrorMessage { get; set; }
    }
}