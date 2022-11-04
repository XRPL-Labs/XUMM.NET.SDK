using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Models.XApp;

public class XummXAppPushResponse
{
    [JsonPropertyName("pushed")]
    public bool Pushed { get; set; }
}