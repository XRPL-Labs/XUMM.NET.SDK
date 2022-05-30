using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Models.Misc
{
    public class XummThirdPartyProfile
    {
        [JsonPropertyName("accountAlias")]
        public string AccountAlias { get; set; } = default!;

        [JsonPropertyName("source")]
        public string Source { get; set; } = default!;
    }
}
