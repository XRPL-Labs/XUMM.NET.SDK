using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Webhooks.Models;

public class XummMeta
{
    [JsonPropertyName("url")]
    public string Url { get; set; } = default!;

    [JsonPropertyName("application_uuidv4")]
    public string ApplicationUuidv4 { get; set; } = default!;

    [JsonPropertyName("payload_uuidv4")]
    public string PayloadUuidv4 { get; set; } = default!;

    [JsonPropertyName("opened_by_deeplink")]
    public bool OpenedByDeeplink { get; set; }
}
