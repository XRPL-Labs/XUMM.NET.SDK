using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Models.Misc;

public class XummTransaction
{
    [JsonPropertyName("txid")]
    public string Txid { get; set; } = default!;

    [JsonPropertyName("balanceChanges")]
    public Dictionary<string, List<XummTransactionBalanceChanges>> BalanceChanges { get; set; } = default!;

    [JsonPropertyName("node")]
    public string Node { get; set; } = default!;

    [JsonPropertyName("transaction")]
    public JsonDocument? Transaction { get; set; }
}
