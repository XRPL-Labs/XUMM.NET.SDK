using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Models.Misc;

internal class XummKycInfo
{
    [JsonPropertyName("account")]
    public string Account { get; set; } = default!;

    [JsonPropertyName("kycApproved")]
    public bool KycApproved { get; set; }
}