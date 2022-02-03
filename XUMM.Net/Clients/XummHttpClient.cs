using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using XUMM.Net.Clients.Interfaces;
using XUMM.Net.Configs;
using XUMM.Net.Models;

namespace XUMM.Net.Clients;

public class XummHttpClient : IXummHttpClient
{
    private readonly ApiConfig _config;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<XummHttpClient> _logger;
    private readonly JsonSerializerOptions _serializerOptions;

    public XummHttpClient(
        IHttpClientFactory httpClientFactory,
        IOptions<ApiConfig> options,
        ILogger<XummHttpClient> logger)
    {
        _config = options.Value;
        _httpClientFactory = httpClientFactory;
        _logger = logger;

        _serializerOptions = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Converters =
            {
                new JsonStringEnumConverter()
            }
        };
    }

    public async Task<T> GetAsync<T>(string endpoint, bool isPublicEndpoint = false)
    {
        return await SendAsync<T>(HttpMethod.Get, endpoint, !isPublicEndpoint, default);
    }

    public async Task<T> PostAsync<T>(string endpoint, object content)
    {
        return await PostAsync<T>(endpoint, JsonSerializer.Serialize(content, _serializerOptions));
    }

    public async Task<T> PostAsync<T>(string endpoint, string json)
    {
        return await SendAsync<T>(HttpMethod.Post, endpoint, true, json);
    }

    public async Task<T> DeleteAsync<T>(string endpoint)
    {
        return await SendAsync<T>(HttpMethod.Delete, endpoint, true, default);
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
        httpClient.DefaultRequestHeaders.Add("User-Agent", "XUMM-Net");
        return httpClient;
    }

    private async Task<T> SendAsync<T>(HttpMethod method, string endpoint, bool setCredentials, string? json)
    {
        try
        {
            using var client = GetHttpClient(setCredentials);
            using var requestMessage = new HttpRequestMessage(method, GetRequestUrl(endpoint));

            if (json != null)
            {
                requestMessage.Content = new StringContent(json, Encoding.UTF8, "application/json");
                requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }

            using var response = await client.SendAsync(requestMessage);
            if (!response.IsSuccessStatusCode)
            {
                throw await GetHttpRequestExceptionAsync(response);
            }

            var result = (T?)await response.Content.ReadFromJsonAsync(typeof(T));
            if (result == null)
            {
                throw new Exception($"Unexpected response for {endpoint} response.");
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Unexpected response from XUMM API [{method}:{endpoint}]");
            throw;
        }
    }

    private async Task<HttpRequestException> GetHttpRequestExceptionAsync(HttpResponseMessage response)
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
            }

            if (exception == null &&
                await response.Content.ReadFromJsonAsync(typeof(XummApiError)) is XummApiError apiError)
            {
                exception = new HttpRequestException(
                    $"Error code: '{apiError.Error.Code}' with message: '{apiError.Error.Message}', see XUMM Dev Console, reference: '{apiError.Error.Reference}'.",
                    null, response.StatusCode);
            }
        }
        catch (Exception ex)
        {
            _logger.LogTrace(ex,
                $"No {nameof(XummFatalApiError)} available in unsuccessful response body of request: {response.RequestMessage?.RequestUri}");
        }

        return exception ??= new HttpRequestException(response.ReasonPhrase, null, response.StatusCode);
    }

    private string GetRequestUrl(string endpoint)
    {
        var result = _config.RestClientAddress;
        if (!result.EndsWith('/'))
        {
            result += "/";
        }

        result += $"platform/{endpoint}";

        return result;
    }
}
