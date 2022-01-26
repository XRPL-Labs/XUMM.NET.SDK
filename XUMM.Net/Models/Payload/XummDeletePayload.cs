using System.Text.Json.Serialization;

namespace XUMM.Net.Models.Payload;

public class XummDeletePayload
{
    [JsonPropertyName("result")]
    public XummDeletePayloadResult Result { get; set; } = default!;

    [JsonPropertyName("meta")]
    public XummPayloadDetailsMeta Meta { get; set; } = default!;

    [JsonPropertyName("custom_meta")]
    public XummPayloadCustomMeta? CustomMeta { get; set; }
}
