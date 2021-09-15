using System;
using System.Security;
using XUMM.Net.Extensions;

namespace XUMM.Net
{
    public class XummApiCredentials : IDisposable
    {
        internal SecureString ApiKey { get; }
        internal SecureString ApiSecret { get; }

        public XummApiCredentials(string apiKey, string apiSecret)
        {
            if (!apiKey.IsValidUuid())
            {
                throw new ArgumentException("A valid API Key must be provided", nameof(apiKey));
            }

            if (!apiSecret.IsValidUuid())
            {
                throw new ArgumentException("A valid API Secret must be provided", nameof(apiSecret));
            }

            ApiKey = apiKey.ToSecureString();
            ApiSecret = apiSecret.ToSecureString();
        }

        public void Dispose()
        {
            ApiKey.Dispose();
            ApiSecret.Dispose();
        }
    }
}
