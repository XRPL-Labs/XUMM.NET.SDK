using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Models.Payload;

public class XummDeletePayloadResult
{
    [JsonPropertyName("cancelled")]
    public bool Cancelled { get; set; }

    [JsonPropertyName("reason")]
    public string Reason { get; set; } = default!;
}
