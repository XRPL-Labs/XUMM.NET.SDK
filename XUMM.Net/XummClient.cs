using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using XUMM.Net.Clients;
using XUMM.Net.Clients.Interfaces;
using XUMM.Net.Extensions;
using XUMM.Net.Models;

namespace XUMM.Net
{
    public class XummClient : IXummClient, IDisposable
    {
        /// <summary>
        /// Miscellaneous endpoints
        /// </summary>
        public IXummClientMisc Misc { get; }

        public XummClientOptions ClientOptions { get; }

        internal readonly ILogger? Logger;

        public XummClient(XummClientOptions options) : this(options, default)
        {

        }

        public XummClient(XummClientOptions options, ILoggerFactory? loggerFactory)
        {
            Misc = new XummClientMisc(this);

            ClientOptions = options ?? throw new ArgumentNullException($"{nameof(options)} cannot be null", nameof(options));
            Logger = loggerFactory.CreateLogger<XummClient>();
        }

        internal async Task<T> GetAsync<T>(string endpoint)
        {
            try
            {
                using var client = GetHttpClient();
                var response = await client.GetAsync($"{ClientOptions.BaseUrl}{endpoint}");
                if (!response.IsSuccessStatusCode)
                {
                    throw await GetHttpRequestException(response);
                }

                return (T)await response.Content.ReadFromJsonAsync(typeof(T));
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, $"Unexpected response from XUMM API [GET:{endpoint}]");
                throw;
            }
        }

        private async Task<HttpRequestException> GetHttpRequestException(HttpResponseMessage response)
        {
            HttpRequestException? exception = null;
            try
            {
                if (await response.Content.ReadFromJsonAsync(typeof(XummFatalApiError)) is XummFatalApiError fatalApiError)
                {
                    if (!string.IsNullOrWhiteSpace(fatalApiError.Message))
                    {
                        exception = new HttpRequestException(fatalApiError.Message, null, response.StatusCode);
                    }
                    else if (fatalApiError.Code != 0)
                    {
                        exception = new HttpRequestException($"Error code ${fatalApiError.Code}, see XUMM Dev Console, reference: ${fatalApiError.Reference}",
                            null, (HttpStatusCode)fatalApiError.Code);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger?.LogTrace(ex, $"No {nameof(XummFatalApiError)} available in unsuccessful response body of request: {response.RequestMessage?.RequestUri}");
            }

            return exception ??= new HttpRequestException(response.ReasonPhrase, null, response.StatusCode);
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
