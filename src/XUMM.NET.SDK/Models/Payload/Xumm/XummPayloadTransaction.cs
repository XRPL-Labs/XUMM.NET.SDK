using System.Collections.Generic;
using XUMM.NET.SDK.Enums;

namespace XUMM.NET.SDK.Models.Payload.Xumm;

public class XummPayloadTransaction : Dictionary<string, object>
{
    public XummPayloadTransaction(XummTransactionType transactionType)
    {
        Add("TransactionType", transactionType.ToString());
    }
}