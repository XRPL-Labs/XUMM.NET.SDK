using System.Text.Json.Serialization;

namespace XUMM.Net.Models.XApp
{
    public class XummXAppOriginResponse
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("data")]
        public XummXAppOriginDataResponse? Data { get; set; }
    }
}
