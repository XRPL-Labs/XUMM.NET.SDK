using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace XUMM.Net.Models.Payload.Xumm
{
    public class XummPayloadTransactionBase
    {
        internal XummPayloadTransactionBase(string transactionType)
        {
            ExtensionData.Add("TransactionType", transactionType);
        }

        [JsonExtensionData]
        public IDictionary<string, object> ExtensionData { get; set; } = new Dictionary<string, object>();
    }
}
