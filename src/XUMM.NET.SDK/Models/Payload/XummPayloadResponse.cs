using System.Text.Json.Serialization;

namespace XUMM.Net.Models.Payload
{
    public class XummPayloadResponse
    {
        [JsonPropertyName("uuid")]
        public string Uuid { get; set; } = default!;

        [JsonPropertyName("next")]
        public XummPayloadNextResponse Next { get; set; } = default!;

        [JsonPropertyName("refs")]
        public XummPayloadRefsResponse Refs { get; set; } = default!;

        [JsonPropertyName("pushed")]
        public bool Pushed { get; set; }
    }
}
