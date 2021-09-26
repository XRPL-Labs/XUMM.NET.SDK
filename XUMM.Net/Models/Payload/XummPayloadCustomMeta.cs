using System.Text.Json.Serialization;

namespace XUMM.Net.Models.Payload
{
    public class XummPayloadCustomMeta
    {
        /// <summary>
        /// Your own identifier for this payload. This identifier must be unique. If duplicate, an error code 409 will be returned (max 40 positions)
        /// </summary>
        [JsonPropertyName("identifier")]
        public string? Identifier { get; set; }

        /// <summary>
        /// A custom JSON object containing metadata, attached to this specific payload (stringified max 1500 positions)
        /// </summary>
        [JsonPropertyName("blob")]
        public string? Blob { get; set; }

        /// <summary>
        /// A message (instruction, reason for signing) to display to the XUMM (signing) user (max 280 positions)
        /// </summary>
        [JsonPropertyName("instruction")]
        public string? Instruction { get; set; }
    }
}
