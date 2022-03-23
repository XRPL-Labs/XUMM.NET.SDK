using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Models.Misc;

public class XummUserTokens
{
    [JsonPropertyName("tokens")]
    public List<XummUserTokenValidity> Tokens { get; set; } = default!;
}
