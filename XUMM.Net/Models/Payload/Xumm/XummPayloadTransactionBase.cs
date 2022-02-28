using System.Collections.Generic;

namespace XUMM.Net.Models.Payload.Xumm
{
    public class XummPayloadTransactionBase : Dictionary<string, object>
    {
        internal XummPayloadTransactionBase(string transactionType)
        {
            Add("TransactionType", transactionType);
        }
    }
}
