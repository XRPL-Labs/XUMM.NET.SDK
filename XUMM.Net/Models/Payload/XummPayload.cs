using System.Text.Json.Serialization;
using XUMM.Net.Models.XrpLedger;

namespace XUMM.Net.Models.Payload
{
    public class XummPayload
    {
        public XummPayload(TransactionCommonFields transaction)
        {
            TransactionCommonFields = transaction;
        }

        /// <summary>
        /// User (Push) token, to deliver a signing request directly to the mobile device of a user (Optional)
        /// </summary>
        [JsonPropertyName("user_token")]
        public string? UserToken { get; set; }

        /// <summary>
        /// Mandatory JSON transaction template to sign. Alternatively a HEX string could be posted in a txblob field.
        /// </summary>
        [JsonPropertyName("txjson")]
        public TransactionCommonFields TransactionCommonFields { get; }

        /// <summary>
        /// You can provide a HEX transaction template instead of a JSON formatted one here.
        /// </summary>
        [JsonPropertyName("txblob")]
        public string? TxBlob { get; set; }

        /// <summary>
        /// Payload options (Optional)
        /// </summary>
        [JsonPropertyName("options")]
        public XummPayloadOptions? Options { get; set; }
    }
}
