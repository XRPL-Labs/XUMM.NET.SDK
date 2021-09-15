using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace XUMM.Net.Models.Misc
{
    public class XummAuth
    {
        [JsonPropertyName("application")]
        public XummApplication Application { get; set; }

        [JsonPropertyName("call")]
        public XummCall Call { get; set; }

        [JsonPropertyName("quota")]
        public Dictionary<string, object> Quota { get; set; }
    }
}
