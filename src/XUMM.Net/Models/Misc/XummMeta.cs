using System.Text.Json.Serialization;

namespace XUMM.Net.Models.Misc
{
    public class XummMeta
    {
        [JsonPropertyName("currency")]
        public XummCurrency Currency { get; set; } = default!;
    }
}