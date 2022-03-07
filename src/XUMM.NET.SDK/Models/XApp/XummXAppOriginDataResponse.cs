using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Models.XApp
{
    public class XummXAppOriginDataResponse
    {
        [JsonPropertyName("payload")]
        public string? Payload { get; set; }
    }
}
