using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Models.XApp;

public class XummXAppEventResponse
{
    [JsonPropertyName("pushed")]
    public bool Pushed { get; set; }

    [JsonPropertyName("uuid")]
    public string? Uuid { get; set; }
}