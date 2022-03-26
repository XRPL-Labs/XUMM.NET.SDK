using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Webhooks.Models;

public class XummWebhookBody
{
    [JsonPropertyName("meta")]
    public XummMeta Meta { get; set; } = default!;

    [JsonPropertyName("custom_meta")]
    public XummCustomMeta CustomMeta { get; set; } = default!;

    [JsonPropertyName("payloadResponse")]
    public XummPayloadResponse PayloadResponse { get; set; } = default!;

    [JsonPropertyName("userToken")]
    public XummUserToken? UserToken { get; set; }
}
