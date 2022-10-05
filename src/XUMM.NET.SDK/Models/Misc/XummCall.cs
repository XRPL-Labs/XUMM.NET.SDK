using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Models.Misc;

public class XummCall
{
    [JsonPropertyName("uuidv4")]
    public string Uuidv4 { get; set; } = default!;
}