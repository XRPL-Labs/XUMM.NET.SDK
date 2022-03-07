using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Models.Misc
{
    public class XummMeta
    {
        [JsonPropertyName("currency")]
        public XummCurrency Currency { get; set; } = default!;
    }
}