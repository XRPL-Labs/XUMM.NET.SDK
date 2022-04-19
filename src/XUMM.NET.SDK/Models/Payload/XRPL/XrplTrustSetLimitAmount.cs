using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Models.Payload.XRPL;

/// <summary>
/// Object defining the trust line to create or modify, in the format of a Currency Amount.
/// </summary>
public class XrplTrustSetLimitAmount
{
    /// <summary>
    /// The currency to this trust line applies to, as a three-letter ISO 4217 Currency Code  or a 160-bit hex value according
    /// to currency format. "XRP" is invalid.
    /// </summary>
    [JsonPropertyName("currency")]
    public string Currency { get; set; } = default!;

    /// <summary>
    /// Quoted decimal representation of the limit to set on this trust line.
    /// </summary>
    [JsonPropertyName("value")]
    public string Value { get; set; } = default!;

    /// <summary>
    /// The address of the account to extend trust to.
    /// </summary>
    [JsonPropertyName("issuer")]
    public string Issuer { get; set; } = default!;
}
