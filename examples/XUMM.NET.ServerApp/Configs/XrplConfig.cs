namespace XUMM.NET.ServerApp.Configs
{
    public class XrplConfig
    {
        internal const string SectionKey = "Xrpl";

        public string Account { get; set; } = default!;
        public string CurrencyCode { get; set; } = default!;
    }
}
