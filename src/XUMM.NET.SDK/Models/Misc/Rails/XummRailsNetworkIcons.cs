using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Models.Misc;

public class XummRailsNetworkIcons
{
    [JsonPropertyName("icon_square")]
    public string IconSquare { get; set; } = default!;

    [JsonPropertyName("icon_asset")]
    public string IconAsset { get; set; } = default!;
}