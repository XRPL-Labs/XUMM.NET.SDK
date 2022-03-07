using System.Collections.Generic;

namespace XUMM.NET.SDK.Models.Payload.Xumm
{
    public class XummPayloadTransactionBase : Dictionary<string, object>
    {
        internal XummPayloadTransactionBase(string transactionType)
        {
            Add("TransactionType", transactionType);
        }
    }
}
