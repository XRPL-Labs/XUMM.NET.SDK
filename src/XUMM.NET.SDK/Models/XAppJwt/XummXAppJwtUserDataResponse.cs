using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Models.XAppJWT;

public class XummXAppJwtUserDataResponse
{
    [JsonPropertyName("operation")]
    public string Operation { get; set; } = default!;

    [JsonPropertyName("data")]
    public Dictionary<string, JsonDocument> Data { get; set; } = default!;

    [JsonPropertyName("keys")]
    public List<string> Keys { get; set; } = default!;

    [JsonPropertyName("count")]
    public int Count { get; set; }
}
