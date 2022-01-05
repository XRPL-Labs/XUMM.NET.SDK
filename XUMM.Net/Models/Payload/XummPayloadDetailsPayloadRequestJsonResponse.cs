using System.Text.Json.Serialization;

namespace XUMM.Net.Models.Payload
{
    public class XummPayloadDetailsPayloadRequestJsonResponse
    {
        [JsonPropertyName("TransactionType")]
        public string TransactionType { get; set; } = default!;

        [JsonPropertyName("Destination")]
        public string Destination { get; set; } = default!;

        [JsonPropertyName("Fee")]
        public string Fee { get; set; } = default!;
    }
}