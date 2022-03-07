using System.Text.Json.Serialization;

namespace XUMM.Net.Models.Misc
{
    public class XummRates
    {
        [JsonPropertyName("USD")]
        public double USD { get; set; }

        [JsonPropertyName("XRP")]
        public double XRP { get; set; }

        [JsonPropertyName("__meta")]
        public XummMeta Meta { get; set; } = default!;
    }
}
