using System.Text.Json.Serialization;

namespace XUMM.Net.Models
{
    public class XummApiErrorDetails
    {
        [JsonPropertyName("reference")]
        public string? Reference { get; set; }

        [JsonPropertyName("code")]
        public int? Code { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; } = default!;
    }
}
