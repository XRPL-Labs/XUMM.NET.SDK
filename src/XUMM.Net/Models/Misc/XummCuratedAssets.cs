using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace XUMM.Net.Models.Misc
{
    public class XummCuratedAssets
    {
        [JsonPropertyName("issuers")]
        public List<string> Issuers { get; set; } = default!;

        [JsonPropertyName("currencies")]
        public List<string> Currencies { get; set; } = default!;

        [JsonPropertyName("details")]
        public Dictionary<string, XummCuratedAssetsDetails> Details { get; set; } = default!;
    }
}
