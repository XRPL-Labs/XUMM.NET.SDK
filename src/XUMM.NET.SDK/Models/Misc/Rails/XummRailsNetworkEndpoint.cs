using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Models.Misc;

public class XummRailsNetworkEndpoint
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("url")]
    public string Url { get; set; } = default!;
}
