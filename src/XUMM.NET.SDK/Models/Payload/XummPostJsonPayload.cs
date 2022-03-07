using System.Text.Json;
using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Models.Payload
{
    public class XummPostJsonPayload : XummPayloadBodyBase
    {
        public XummPostJsonPayload(string txJson)
        {
            TxJson = JsonDocument.Parse(txJson);
        }

        /// <summary>
        /// Mandatory JSON transaction template to sign. Alternatively a HEX string could be posted with <see cref="XummPostBlobPayload"/>.
        /// </summary>
        [JsonPropertyName("txjson")]
        public JsonDocument TxJson { get; }
    }
}
