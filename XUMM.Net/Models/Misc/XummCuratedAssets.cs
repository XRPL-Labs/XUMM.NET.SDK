using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace XUMM.Net.Models.Misc
{
    public class XummCuratedAssets
    {
        [JsonPropertyName("issuers")]
        public List<string> Issuers { get; set; }

        [JsonPropertyName("currencies")]
        public List<string> Currencies { get; set; }

        [JsonPropertyName("details")]
        public Dictionary<string, XummCuratedAssetsDetails> Details { get; set; }
    }
}
