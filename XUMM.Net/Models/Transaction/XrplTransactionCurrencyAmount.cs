using System.Text.Json.Serialization;

namespace XUMM.Net.Models.Transaction
{
    public class XrplTransactionCurrencyAmount
    {
        [JsonPropertyName("Amount")]
        public string Amount { get; set; } = default!;

        [JsonPropertyName("Currency")]
        public string? Currency { get; set; }

        [JsonPropertyName("Issuer")]
        public string? Issuer { get; set; }
    }
}
