using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Models.Payload.XRPL
{
    public class XrplTransactionSignerField
    {

        [JsonPropertyName("Account")]
        public string Account { get; set; } = default!;

        [JsonPropertyName("TxnSignature")]
        public string TxnSignature { get; set; } = default!;

        [JsonPropertyName("SigningPubKey")]
        public string SigningPubKey { get; set; } = default!;
    }
}
