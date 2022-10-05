using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Models;

public class XummApiError
{
    [JsonPropertyName("error")]
    public XummApiErrorDetails Error { get; set; } = default!;
}