using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Models.Misc
{
    internal class XummKycStatusInfo
    {
        [JsonPropertyName("kycStatus")]
        public string KycStatus { get; set; } = default!;
    }
}
