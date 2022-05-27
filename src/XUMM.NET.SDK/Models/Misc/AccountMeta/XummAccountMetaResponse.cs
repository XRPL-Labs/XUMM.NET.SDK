using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Models.Misc
{
    public class XummAccountMetaResponse
    {
        [JsonPropertyName("account")]
        public string Account { get; set; } = default!;

        [JsonPropertyName("kycApproved")]
        public bool KycApproved { get; set; }

        [JsonPropertyName("xummPro")]
        public bool XummPro { get; set; }

        [JsonPropertyName("avatar")]
        public string? Avatar { get; set; }

        [JsonPropertyName("xummProfile")]
        public XummProfile? XummProfile { get; set; } 

        [JsonPropertyName("thirdPartyProfiles")]
        public List<XummThirdPartyProfile> ThirdPartyProfiles { get; set; } = new List<XummThirdPartyProfile>();

        [JsonPropertyName("globalid")]
        public XummGlobaliD? GlobaliD { get; set; }
    }
}
