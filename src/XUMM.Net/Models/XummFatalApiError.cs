using System.Text.Json.Serialization;

namespace XUMM.Net.Models
{
    public class XummFatalApiError
    {
        [JsonPropertyName("error")]
        public bool Error { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; } = default!;

        [JsonPropertyName("reference")]
        public string? Reference { get; set; }

        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("req")]
        public string? Request { get; set; }

        [JsonPropertyName("method")]
        public string? Method { get; set; }
    }
}
