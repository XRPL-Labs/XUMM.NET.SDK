using XUMM.NET.SDK.Enums;

namespace XUMM.NET.SDK.Models.Payload.Xumm
{
    public class XummPayloadTransaction : XummPayloadTransactionBase
    {
        public XummPayloadTransaction(XummTransactionType transactionType) : base(transactionType.ToString())
        {
        }
    }
}
