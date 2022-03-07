using System.Text.Json.Serialization;

namespace XUMM.Net.Models.XApp
{
    public class XummXAppOriginDataResponse
    {
        [JsonPropertyName("payload")]
        public string? Payload { get; set; }
    }
}
