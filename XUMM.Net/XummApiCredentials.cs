using System;
using System.Security;
using XUMM.Net.Extensions;

namespace XUMM.Net
{
    public class XummApiCredentials : IDisposable
    {
        public SecureString ApiKey { get; }
        public SecureString ApiSecret { get; }

        public XummApiCredentials(string apiKey, string apiSecret)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new ArgumentException($"{nameof(apiKey)} cannot be null or whitespace", nameof(apiKey));
            }

            if (string.IsNullOrWhiteSpace(apiSecret))
            {
                throw new ArgumentException($"{nameof(apiSecret)} cannot be null or whitespace", nameof(apiSecret));
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
