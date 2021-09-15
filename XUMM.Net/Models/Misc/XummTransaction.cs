using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace XUMM.Net.Models.Misc
{
    public class XummTransaction
    {
        [JsonPropertyName("txid")]
        public string Txid { get; set; }

        [JsonPropertyName("balanceChanges")]
        public Dictionary<string, List<XummTransactionBalanceChanges>> BalanceChanges { get; set; }

        [JsonPropertyName("node")]
        public string Node { get; set; }

        [JsonPropertyName("transaction")]
        public Dictionary<string, object> Transaction { get; set; }
    }
}
