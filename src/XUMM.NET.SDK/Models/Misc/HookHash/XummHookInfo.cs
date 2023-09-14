using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Models.Misc;

public class XummHookInfo
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("description")]
    public string Description { get; set; } = default!;

    [JsonPropertyName("creator")]
    public XummHookInfoCreator? Creator { get; set; } 

    [JsonPropertyName("xapp")]
    public string? Xapp { get; set; }

    [JsonPropertyName("appuuid")]
    public string? AppUuid { get; set; }

    [JsonPropertyName("icon")]
    public string? Icon { get; set; }

    [JsonPropertyName("verifiedAccounts")]
    public List<string>? VerifiedAccounts { get; set; }

    [JsonPropertyName("audits")]
    public List<string>? Audits { get; set; }
}
