using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Models.Misc;

public class XummProfile
{
    [JsonPropertyName("accountAlias")]
    public string? AccountAlias { get; set; }

    [JsonPropertyName("ownerAlias")]
    public string? OwnerAlias { get; set; }
}