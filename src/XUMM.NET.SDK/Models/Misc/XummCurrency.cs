using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Models.Misc
{
    public class XummCurrency
    {
        [JsonPropertyName("en")]
        public string En { get; set; } = default!;

        [JsonPropertyName("code")]
        public string Code { get; set; } = default!;

        [JsonPropertyName("symbol")]
        public string? Symbol { get; set; }

        [JsonPropertyName("isoDecimals")]
        public int IsoDecimals { get; set; }
    }
}
