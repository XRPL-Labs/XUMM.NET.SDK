using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Models.Misc;

public class XummUserTokenValidity
{
    [JsonPropertyName("user_token")]
    public string UserToken { get; set; } = default!;

    [JsonPropertyName("active")]
    public bool Active { get; set; }

    [JsonPropertyName("issued")]
    public int? Issued { get; set; }

    [JsonPropertyName("expires")]
    public int? Expires { get; set; }
}
