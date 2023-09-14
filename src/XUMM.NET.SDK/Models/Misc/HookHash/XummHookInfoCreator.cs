using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Models.Misc;

public class XummHookInfoCreator
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("mail")]
    public string? Mail { get; set; }

    [JsonPropertyName("site")]
    public string? Site { get; set; }
}
