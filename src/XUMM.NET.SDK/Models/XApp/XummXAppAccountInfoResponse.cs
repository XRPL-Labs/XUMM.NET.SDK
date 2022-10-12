using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Models.XApp;

public class XummXAppAccountInfoResponse
{
    [JsonPropertyName("account")]
    public string Account { get; set; } = default!;

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("domain")]
    public string? Domain { get; set; }

    [JsonPropertyName("blocked")]
    public bool Blocked { get; set; } 

    [JsonPropertyName("source")]
    public string Source { get; set; } = default!;

    [JsonPropertyName("kycApproved")]
    public bool KycApproved { get; set; }

    [JsonPropertyName("proSubscription")]
    public bool ProSubscription { get; set; }
}