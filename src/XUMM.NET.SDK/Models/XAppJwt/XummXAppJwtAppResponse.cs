using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Models.XAppJWT;

public class XummXAppJwtAppResponse
{
    [JsonPropertyName("name")] 
    public string Name { get; set; } = default!;
}
