namespace XUMM.Net
{
    public class XummApiAddress
    {
        public string RestClientAddress { get; set; } = default!;

        /// <summary>
        /// The default XUMM REST API settings
        /// </summary>
        public static XummApiAddress Default = new()
        {
            RestClientAddress = "https://xumm.app/api/v1",
        };
    }
}
