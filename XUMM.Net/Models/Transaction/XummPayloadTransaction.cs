using XUMM.Net.Enums;

namespace XUMM.Net.Models.Transaction
{
    public class XummPayloadTransaction : XummPayloadTransactionBase
    {
        public XummPayloadTransaction(XummTransactionType transactionType) : base(transactionType.ToString())
        {
        }
    }
}
