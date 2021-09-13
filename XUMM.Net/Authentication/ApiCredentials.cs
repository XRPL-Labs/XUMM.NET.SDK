using System;

namespace XUMM.Net.Authentication
{
    public class ApiCredentials
    {
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }

        public ApiCredentials(string apiKey, string apiSecret)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new ArgumentException($"{nameof(apiKey)} cannot be null or whitespace", nameof(apiKey));
            }

            if (string.IsNullOrWhiteSpace(apiSecret))
            {
                throw new ArgumentException($"{nameof(apiSecret)} cannot be null or whitespace", nameof(apiSecret));
            }

            ApiKey = apiKey;
            ApiSecret = apiSecret;
        }
    }
}
