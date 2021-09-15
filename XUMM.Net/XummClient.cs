using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
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

                response.EnsureSuccessStatusCode();

                return (T)await response.Content.ReadFromJsonAsync(typeof(T));
            }
            catch (Exception ex)
            {
                if (ex is HttpRequestException { StatusCode: HttpStatusCode.InternalServerError })
                {
                    TryLogFatalApiError(ex.Message);
                }

                Logger?.LogError(ex, $"Unexpected response from XUMM API [GET:{endpoint}]");
                throw;
            }
        }

        private void TryLogFatalApiError(string json)
        {
            try
            {
                var fatalApiError = JsonSerializer.Deserialize<XummFatalApiError>(json);
                if (fatalApiError != null)
                {
                    if (!string.IsNullOrWhiteSpace(fatalApiError.Message))
                    {
                        Logger?.LogError(fatalApiError.Message);
                    }
                    else if (fatalApiError.Code != 0)
                    {
                        Logger?.LogError($"Error code ${fatalApiError.Code}, see XUMM Dev Console, reference: ${fatalApiError.Reference}");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "Failed to log a fatal API error");
            }
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
