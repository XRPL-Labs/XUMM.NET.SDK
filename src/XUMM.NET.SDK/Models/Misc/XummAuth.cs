using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Models.Misc;

public class XummAuth
{
    [JsonPropertyName("application")]
    public XummApplication Application { get; set; } = default!;

    [JsonPropertyName("call")]
    public XummCall Call { get; set; } = default!;

    [JsonPropertyName("quota")]
    public Dictionary<string, object> Quota { get; set; } = default!;
}