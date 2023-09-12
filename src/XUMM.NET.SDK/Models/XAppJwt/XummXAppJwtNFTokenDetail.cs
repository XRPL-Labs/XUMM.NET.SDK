using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Models.XAppJwt;

public class XummXAppJwtNFTokenDetail
{
    [JsonPropertyName("issuer")]
    public string? Issuer { get; set; }

    [JsonPropertyName("token")]
    public string? Token { get; set; }

    [JsonPropertyName("owner")]
    public string? Owner { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("image")]
    public string? Image { get; set; }
}
