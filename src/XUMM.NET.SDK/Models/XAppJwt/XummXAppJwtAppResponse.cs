using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Models.XAppJwt;

public class XummXAppJwtAppResponse
{
    [JsonPropertyName("name")] 
    public string Name { get; set; } = default!;
}
