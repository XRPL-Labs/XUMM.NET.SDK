using System.Text.Json.Serialization;

namespace XUMM.Net.Models.XrpLedger
{
    public class TransactionMemoField
    {

        [JsonPropertyName("MemoData")]
        public string MemoData { get; set; } = default!;

        [JsonPropertyName("MemoFormat")]
        public string MemoFormat { get; set; } = default!;

        [JsonPropertyName("MemoType")]
        public string MemoType { get; set; } = default!;
    }
}
