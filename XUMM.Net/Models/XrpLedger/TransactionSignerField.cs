using System.Text.Json.Serialization;

namespace XUMM.Net.Models.XrpLedger
{
    public class TransactionSignerField
    {

        [JsonPropertyName("Account")]
        public string Account { get; set; } = default!;

        [JsonPropertyName("TxnSignature")]
        public string TxnSignature { get; set; } = default!;

        [JsonPropertyName("SigningPubKey")]
        public string SigningPubKey { get; set; } = default!;
    }
}
