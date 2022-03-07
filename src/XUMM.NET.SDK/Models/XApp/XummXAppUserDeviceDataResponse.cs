using System.Text.Json.Serialization;

namespace XUMM.Net.Models.XApp
{
    public class XummXAppUserDeviceDataResponse
    {
        [JsonPropertyName("currency")]
        public string? Currency { get; set; }
    }
}
