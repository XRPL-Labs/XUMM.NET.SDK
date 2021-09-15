using System;

namespace XUMM.Net
{
    public class XummApiAddress
    {
        public string RestClientAddress { get; }

        public XummApiAddress(string restClientAddress)
        {
            RestClientAddress = restClientAddress ?? throw new ArgumentNullException(nameof(restClientAddress));
        }

        /// <summary>
        /// The default XUMM REST API settings
        /// </summary>
        public static XummApiAddress Default = new("https://xumm.app/api/v1");
    }
}
