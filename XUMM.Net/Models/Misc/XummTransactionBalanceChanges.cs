using System.Text.Json.Serialization;

namespace XUMM.Net.Models.Misc
{
    public class XummTransactionBalanceChanges
    {
        [JsonPropertyName("counterparty")]
        public string CounterParty { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("formatted")]
        public XummTransactionBalanceChangesFormatted Formatted { get; set; }
    }
}
