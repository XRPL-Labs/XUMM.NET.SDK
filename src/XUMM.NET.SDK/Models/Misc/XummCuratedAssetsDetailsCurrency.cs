using System.Text.Json.Serialization;
using XUMM.NET.SDK.Extensions;

namespace XUMM.NET.SDK.Models.Misc;

public class XummCuratedAssetsDetailsCurrency
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("issuer_id")]
    public int IssuerId { get; set; }

    [JsonPropertyName("issuer")]
    public string Issuer { get; set; } = default!;

    [JsonPropertyName("currency")]
    public string Currency { get; set; } = default!;

    /// <summary>
    /// Formatted Currency Code supporting 3 chars, HEX and XLS15d HEX currency codes
    /// </summary>
    public string CurrencyFormatted => Currency.ToFormattedCurrency();

    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("avatar")]
    public string Avatar { get; set; } = default!;

    [JsonPropertyName("shortlist")]
    public int Shortlist { get; set; }
}
