using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Models.Misc;

public class XummTransactionBalanceChanges
{
    [JsonPropertyName("counterparty")]
    public string CounterParty { get; set; } = default!;

    [JsonPropertyName("currency")]
    public string Currency { get; set; } = default!;

    [JsonPropertyName("value")]
    public string Value { get; set; } = default!;

    [JsonPropertyName("formatted")]
    public XummTransactionBalanceChangesFormatted Formatted { get; set; } = default!;
}