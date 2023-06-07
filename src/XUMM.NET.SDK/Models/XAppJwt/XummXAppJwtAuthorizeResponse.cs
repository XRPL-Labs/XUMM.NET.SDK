using System.Text.Json.Serialization;
using XUMM.NET.SDK.Models.XApp;
using XUMM.NET.SDK.Models.XAppJWT;

namespace XUMM.NET.SDK.Models.XAppJwt;

public class XummXAppJwtAuthorizeResponse
{
    [JsonPropertyName("ott")] 
    public XummXAppOttResponse OTT { get; set; } = default!;

    [JsonPropertyName("app")]
    public XummXAppJwtAppResponse App { get; set; } = default!;

    [JsonPropertyName("jwt")]
    public string JWT { get; set; } = default!;
}
