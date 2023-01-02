using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Models.Payload.XRPL;

public class XrplTransaction
{
    [JsonPropertyName("Account")]
    public string? Account { get; set; }

    [JsonPropertyName("TransactionType")]
    public string TransactionType { get; set; } = default!;

    [JsonPropertyName("Fee")]
    public string Fee { get; set; } = default!;

    [JsonPropertyName("Sequence")]
    public int? Sequence { get; set; }

    [JsonPropertyName("AccountTxnID")]
    public string? AccountTxnId { get; set; }

    [JsonPropertyName("Flags")]
    public int? Flags { get; set; }

    [JsonPropertyName("LastLedgerSequence")]
    public int? LastLedgerSequence { get; set; }

    [JsonPropertyName("Memos")]
    public List<XrplTransactionMemoField>? Memos { get; set; }

    [JsonPropertyName("Signers")]
    public List<XrplTransactionSignerField>? Signers { get; set; }

    [JsonPropertyName("SourceTag")]
    public int? SourceTag { get; set; }

    [JsonPropertyName("SigningPubKey")]
    public string? SigningPubKey { get; set; }

    [JsonPropertyName("TicketSequence")]
    public int? TicketSequence { get; set; }

    [JsonPropertyName("TxnSignature")]
    public string? TxnSignature { get; set; }
}
