using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Models.Misc;

public class XummRailsNetwork
{
    [JsonPropertyName("chain_id")]
    public int ChainId { get; set; }

    [JsonPropertyName("color")]
    public string Color { get; set; } = default!;

    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("is_livenet")]
    public bool IsLivenet { get; set; }

    [JsonPropertyName("native_asset")]
    public string NativeAsset { get; set; } = default!;

    [JsonPropertyName("endpoints")]
    public List<XummRailsNetworkEndpoint> Endpoints { get; set; } = default!;

    [JsonPropertyName("explorers")]
    public List<XummRailsNetworkExplorer> Explorers { get; set; } = default!;

    [JsonPropertyName("rpc")]
    public string? Rpc { get; set; } 

    [JsonPropertyName("definitions")]
    public string? Definitions { get; set; }

    [JsonPropertyName("icons")]
    public XummRailsNetworkIcons Icons { get; set; } = default!;
}
