using System.Text.Json.Serialization;

namespace XUMM.Net.Models.Payload
{
    public class XummPayloadDetailsPayloadRequestJsonResponse
    {
        [JsonPropertyName("TransactionType")]
        public string TransactionType { get; set; }

        [JsonPropertyName("Destination")]
        public string Destination { get; set; }

        [JsonPropertyName("Fee")]
        public string Fee { get; set; }
    }
}