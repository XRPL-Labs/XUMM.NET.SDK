using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Models.Misc;

public class XummRailsNetworkExplorer
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("url_tx")]
    public string UrlTx { get; set; } = default!;

    [JsonPropertyName("url_account")]
    public string? UrlAccount { get; set; }

    [JsonPropertyName("url_ctid")]
    public string? UrlCtid { get; set; }
}
