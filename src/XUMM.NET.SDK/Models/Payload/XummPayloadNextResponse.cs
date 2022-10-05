using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Models.Payload;

public class XummPayloadNextResponse
{
    [JsonPropertyName("always")]
    public string Always { get; set; } = default!;

    [JsonPropertyName("no_push_msg_received")]
    public string? NoPushMessageReceived { get; set; }
}