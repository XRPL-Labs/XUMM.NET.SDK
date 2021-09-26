using System.Collections.Generic;
using System.Text.Json.Serialization;
using XUMM.Net.Enums;

namespace XUMM.Net.Models.XrpLedger
{
    public class TransactionCommonFields
    {
        public TransactionCommonFields(XrpTransactionType transactionType, string destination, int fee)
        {
            TransactionType = transactionType;
            Destination = destination;
            Fee = fee.ToString();
        }

        [JsonPropertyName("Account")]
        public string? Account { get; set; }

        [JsonPropertyName("TransactionType")]
        public XrpTransactionType TransactionType { get; }

        [JsonPropertyName("Destination")]
        public string Destination { get; }

        [JsonPropertyName("Fee")]
        public string Fee { get; }

        [JsonPropertyName("Sequence")]
        public int? Sequence { get; set; }

        [JsonPropertyName("AccountTxnID")]
        public string? AccountTxnID { get; set; }

        [JsonPropertyName("Flags")]
        public int? Flags { get; set; }

        [JsonPropertyName("LastLedgerSequence")]
        public int? LastLedgerSequence { get; set; }

        [JsonPropertyName("Memos")]
        public List<TransactionMemoField>? Memos { get; set; }

        [JsonPropertyName("Signers")]
        public List<TransactionSignerField>? Signers { get; set; }

        [JsonPropertyName("SourceTag")]
        public int? SourceTag { get; set; }

        [JsonPropertyName("SigningPubKey")]
        public string? SigningPubKey { get; set; }

        [JsonPropertyName("TicketSequence")]
        public int? TicketSequence { get; set; }

        [JsonPropertyName("TxnSignature")]
        public string? TxnSignature { get; set; }
    }
}
