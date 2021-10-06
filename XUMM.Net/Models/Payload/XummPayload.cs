using System.Text.Json;
using System.Text.Json.Serialization;

namespace XUMM.Net.Models.Payload
{
    public class XummPayload
    {
        public XummPayload(string? txJson, string? txBlob)
        {
            if (txJson != null)
            {
                TxJson = JsonDocument.Parse(txJson);
            }

            TxBlob = txBlob;
        }

        /// <summary>
        /// User (Push) token, to deliver a signing request directly to the mobile device of a user (Optional)
        /// </summary>
        [JsonPropertyName("user_token")]
        public string? UserToken { get; set; }

        /// <summary>
        /// Mandatory JSON transaction template to sign. Alternatively a HEX string could be posted in a <see cref="TxBlob"/> field.
        /// </summary>
        [JsonPropertyName("txjson")]
        public JsonDocument? TxJson { get; }

        /// <summary>
        /// You can provide a HEX transaction template instead of a JSON formatted one here.
        /// </summary>
        [JsonPropertyName("txblob")]
        public string? TxBlob { get; }

        /// <summary>
        /// Payload options (Optional)
        /// </summary>
        [JsonPropertyName("options")]
        public XummPayloadOptions? Options { get; set; }

        /// <summary>
        /// Attach information (custom identifier, meta object, user instruction text) to the payload
        /// </summary>
        [JsonPropertyName("custom_meta")]
        public XummPayloadCustomMeta? CustomMeta { get; set; }
    }
}
