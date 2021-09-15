using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using XUMM.Net.Clients;
using XUMM.Net.Clients.Interfaces;
using XUMM.Net.Extensions;

namespace XUMM.Net
{
    public class XummClient : IXummClient, IDisposable
    {
        public XummClientOptions ClientOptions { get; }

        /// <summary>
        /// Miscellaneous endpoints
        /// </summary>
        public IXummClientMisc Misc { get; }

        public XummClient(XummClientOptions options)
        {
            ClientOptions = options ?? throw new ArgumentNullException($"{nameof(options)} cannot be null", nameof(options));

            Misc = new XummClientMisc(this);
        }

        internal async Task<T> ReadResponseAsync<T>(string relativePath)
        {
            using var client = GetHttpClient();
            var response = await client.GetAsync($"{ClientOptions.BaseUrl}{relativePath}");

            response.EnsureSuccessStatusCode();

            return (T)(await response.Content.ReadFromJsonAsync(typeof(T)));
        }

        private HttpClient GetHttpClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-API-Key", ClientOptions.Credentials.ApiKey.GetString());
            client.DefaultRequestHeaders.Add("X-API-Secret", ClientOptions.Credentials.ApiSecret.GetString());
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("User-Agent", "XUMM-Net");
            return client;
        }

        public virtual void Dispose()
        {
            ClientOptions.Credentials.Dispose();
        }
    }
}
