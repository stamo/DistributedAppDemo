using System.Text.Json.Serialization;

namespace Cinema.Infrastructure.Models
{
    public class AuditMessage
    {
        [JsonPropertyName("message")]
        public required string Message { get; set; }

        [JsonPropertyName("sessionId")]
        public required string SessionId { get; set; }

        [JsonPropertyName("resultCode")]
        public int ResultCode { get; set; }
    }
}
