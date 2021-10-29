namespace XUMM.Net
{
    public class XummClientOptions
    {
        public XummApiCredentials Credentials { get; set; }

        public string BaseUrl { get; }

        /// <summary>
        /// Constructor with the default endpoint
        /// </summary>
        public XummClientOptions(XummApiCredentials credentials) : this(XummApiAddress.Default, credentials)
        {
        }

        public XummClientOptions(XummApiAddress address, XummApiCredentials credentials)
        {
            Credentials = credentials;

            BaseUrl = address.RestClientAddress;
            if (!BaseUrl.EndsWith('/'))
            {
                BaseUrl += "/";
            }

            BaseUrl += "platform/";
        }
    }
}
