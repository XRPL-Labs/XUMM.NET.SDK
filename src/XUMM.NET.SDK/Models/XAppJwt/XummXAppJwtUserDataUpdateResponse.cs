using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Models.XAppJwt;

public class XummXAppJwtUserDataUpdateResponse
{
    [JsonPropertyName("operation")]
    public string Operation { get; set; } = default!;

    [JsonPropertyName("persisted")]
    public bool Persisted { get; set; }
}
