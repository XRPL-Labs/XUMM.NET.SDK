using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Models.XApp;

public class XummXAppUserDeviceDataResponse
{
    [JsonPropertyName("currency")]
    public string? Currency { get; set; }
}