using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Models.Payload.XRPL
{
    public class XrplTransactionCurrencyAmount
    {
        [JsonPropertyName("value")]
        public string Value { get; set; } = default!;

        [JsonPropertyName("currency")]
        public string? Currency { get; set; }

        [JsonPropertyName("issuer")]
        public string? Issuer { get; set; }
    }
}
