using System.Text.Json.Serialization;
using XUMM.Net.Models.Misc;

namespace XUMM.Net.Models.Payload
{
    public class XummPayloadDetails
    {
        [JsonPropertyName("meta")]
        public XummPayloadDetailsMeta Meta { get; set; } = default!;

        [JsonPropertyName("application")]
        public XummApplication Application { get; set; } = default!;

        [JsonPropertyName("payload")]
        public XummPayloadDetailsPayloadResponse Payload { get; set; } = default!;

        [JsonPropertyName("response")]
        public XummPayloadDetailsResponse Response { get; set; } = default!;

        /// <summary>
        /// Attach information (custom identifier, meta object, user instruction text) to the payload
        /// </summary>
        [JsonPropertyName("custom_meta")]
        public XummPayloadCustomMeta? CustomMeta { get; set; }
    }
}
