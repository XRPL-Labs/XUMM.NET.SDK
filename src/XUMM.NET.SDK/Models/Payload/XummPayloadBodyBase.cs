using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Models.Payload
{
    public class XummPayloadBodyBase
    {
        /// <summary>
        /// User (Push) token, to deliver a signing request directly to the mobile device of a user (Optional)
        /// </summary>
        [JsonPropertyName("user_token")]
        public string? UserToken { get; set; }

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
