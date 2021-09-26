using XUMM.Net.Enums;

namespace XUMM.Net.ClientConsole.Configs
{
    public class PayloadConfig
    {
        public XrpTransactionType TransactionType { get; set; }
        public string Destination { get; set; }
        public int Fee { get; set; }
    }
}
