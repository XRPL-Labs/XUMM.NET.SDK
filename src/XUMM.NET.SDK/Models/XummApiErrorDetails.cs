using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Models
{
    public class XummApiErrorDetails
    {
        [JsonPropertyName("reference")]
        public string? Reference { get; set; }

        [JsonPropertyName("code")]
        public int? Code { get; set; }
    }
}
