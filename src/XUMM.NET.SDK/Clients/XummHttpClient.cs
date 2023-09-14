using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using XUMM.NET.SDK.Clients.Interfaces;
using XUMM.NET.SDK.Configs;
using XUMM.NET.SDK.Helpers;
using XUMM.NET.SDK.Models;

namespace XUMM.NET.SDK.Clients;

public class XummHttpClient : IXummHttpClient
{
    private static readonly string? UserAgentVersion = typeof(XummHttpClient).Assembly.GetName().Version?.ToString();

    private readonly ApiConfig _config;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<XummHttpClient> _logger;

    public XummHttpClient(
        IHttpClientFactory httpClientFactory,
        IOptions<ApiConfig> options,
        ILogger<XummHttpClient> logger)
    {
        _config = options.Value;
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task<T> GetAsync<T>(HttpClient client, string endpoint)
    {
        return await SendAsync<T>(client, HttpMethod.Get, endpoint, default);
    }

    public async Task<T> GetAsync<T>(string endpoint)
    {
        return await SendAsync<T>(HttpMethod.Get, endpoint, true, default);
    }

    public async Task<T> GetPublicAsync<T>(string endpoint)
    {
        return await SendAsync<T>(HttpMethod.Get, endpoint, false, default);
    }

    public async Task<T> PostAsync<T>(string endpoint, object content)
    {
        return await PostAsync<T>(endpoint, JsonSerializer.Serialize(content, JsonHelper.SerializerOptions));
    }

    public async Task<T> PostAsync<T>(string endpoint, string json)
    {
        return await SendAsync<T>(HttpMethod.Post, endpoint, true, json);
    }

    public async Task<T> PostAsync<T>(HttpClient client, string endpoint, string json)
    {
        return await SendAsync<T>(client, HttpMethod.Post, endpoint, json);
    }

    public async Task<T> DeleteAsync<T>(string endpoint)
    {
        return await SendAsync<T>(HttpMethod.Delete, endpoint, true, default);
    }

    public async Task<T> DeleteAsync<T>(HttpClient client, string endpoint)
    {
        return await SendAsync<T>(client, HttpMethod.Delete, endpoint, default);
    }

    public HttpClient GetHttpClient(bool setCredentials)
    {
        var httpClient = _httpClientFactory.CreateClient();
        httpClient.DefaultRequestHeaders.Clear();

        if (setCredentials)
        {
            httpClient.DefaultRequestHeaders.Add("X-API-Key", _config.ApiKey);
            httpClient.DefaultRequestHeaders.Add("X-API-Secret", _config.ApiSecret);
        }

        httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        httpClient.DefaultRequestHeaders.Add("User-Agent", $"XummDotNet/{UserAgentVersion}");
        return httpClient;
    }

    private Task<T> SendAsync<T>(HttpMethod method, string endpoint, bool setCredentials, string? json)
    {
        var client = GetHttpClient(setCredentials);
        return SendAsync<T>(client, method, endpoint, json);
    }

    private async Task<T> SendAsync<T>(HttpClient client, HttpMethod method, string endpoint, string? json)
    {
        try
        {
            using var requestMessage = new HttpRequestMessage(method, GetRequestUrl(endpoint));

            if (json != null)
            {
                requestMessage.Content = new StringContent(json, Encoding.UTF8, "application/json");
                requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }

            using var response = await client.SendAsync(requestMessage);
            var responseText = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw GetHttpRequestException(response, responseText);
            }

            var result = JsonSerializer.Deserialize<T>(responseText);
            if (result == null)
            {
                throw new Exception($"Unexpected response for {endpoint} response");
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Unexpected response from XUMM API [{method}:{endpoint}]");
            throw;
        }
    }

    private HttpRequestException GetHttpRequestException(HttpResponseMessage response, string responseText)
    {
        HttpRequestException? exception = null;

        try
        {
            var fatalApiError = JsonSerializer.Deserialize<XummFatalApiError>(responseText);
            if (fatalApiError != null)
            {
                if (!string.IsNullOrWhiteSpace(fatalApiError.Message))
                {
#if NET5_0_OR_GREATER
                    exception = new HttpRequestException(fatalApiError.Message, null, response.StatusCode);
#else
                    exception = new HttpRequestException(fatalApiError.Message);
#endif
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogTrace(ex, $"No {nameof(XummFatalApiError)} available in unsuccessful response body of request: {response.RequestMessage?.RequestUri}");
        }

        try
        {
            if (exception == null)
            {
                var apiError = JsonSerializer.Deserialize<XummApiError>(responseText);
                if (apiError?.Error != null)
                {
                    var message = $"Error code {apiError.Error.Code}, see XUMM Dev Console, reference: '{apiError.Error.Reference}'.";

#if NET5_0_OR_GREATER
                    exception = new HttpRequestException(message, null, response.StatusCode);
#else
                    exception = new HttpRequestException(message);
#endif
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogTrace(ex, $"No {nameof(XummApiError)} available in unsuccessful response body of request: {response.RequestMessage?.RequestUri}");
        }

#if NET5_0_OR_GREATER
        exception ??= new HttpRequestException(response.ReasonPhrase, null, response.StatusCode);
#else
        exception ??= new HttpRequestException(response.ReasonPhrase);
#endif

        return exception;
    }

    private string GetRequestUrl(string endpoint)
    {
        var result = _config.RestClientAddress;
        if (!result.EndsWith('/'))
        {
            result += "/";
        }

        result += endpoint;
        return result;
    }
}
