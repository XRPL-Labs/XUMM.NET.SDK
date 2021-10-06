using System.Text.Json.Serialization;

namespace XUMM.Net.Models.Payload.XRPL
{
    public class XrplTransactionMemoField
    {
        [JsonPropertyName("MemoData")]
        public string MemoData { get; set; } = default!;

        [JsonPropertyName("MemoFormat")]
        public string MemoFormat { get; set; } = default!;

        [JsonPropertyName("MemoType")]
        public string MemoType { get; set; } = default!;
    }
}
    