using XUMM.Net.Enums;

namespace XUMM.Net.Models.Payload.Xumm
{
    public class XummPayloadTransaction : XummPayloadTransactionBase
    {
        public XummPayloadTransaction(XummTransactionType transactionType) : base(transactionType.ToString())
        {
        }
    }
}
