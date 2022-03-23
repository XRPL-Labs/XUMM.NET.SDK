using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Models.Misc;

public class XummUserTokensRequest
{
    [JsonPropertyName("tokens")]
    public List<string> Tokens { get; set; } = default!;
}
