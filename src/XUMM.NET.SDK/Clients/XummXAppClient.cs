using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using XUMM.NET.SDK.Clients.Interfaces;
using XUMM.NET.SDK.Configs;
using XUMM.NET.SDK.Extensions;
using XUMM.NET.SDK.Models.XApp;

namespace XUMM.NET.SDK.Clients;

/// <summary>
/// Represents the client for miscellaneous API calls.
/// </summary>
public class XummXAppClient : IXummXAppClient
{
    private readonly IXummHttpClient _httpClient;
    private readonly ApiConfig _config;

    /// <summary>
    /// Initializes a new instance of the <see cref="XummXAppClient"/> class.
    /// </summary>
    public XummXAppClient(IXummHttpClient httpClient,
        IOptions<ApiConfig> options)
    {
        _httpClient = httpClient;
        _config = options.Value;
    }

    /// <inheritdoc />
    public async Task<XummXAppOttResponse> GetOneTimeTokenDataAsync(string oneTimeToken)
    {
        if (string.IsNullOrWhiteSpace(oneTimeToken))
        {
            throw new ArgumentException("Value cannot be null or white space", nameof(oneTimeToken));
        }

        return await _httpClient.GetAsync<XummXAppOttResponse>($"xapp/ott/{oneTimeToken}");
    }

    /// <inheritdoc />
    public async Task<XummXAppOttResponse> ReFetchOneTimeTokenDataAsync(string oneTimeToken, string deviceId)
    {
        if (string.IsNullOrWhiteSpace(oneTimeToken))
        {
            throw new ArgumentException("Value cannot be null or white space", nameof(oneTimeToken));
        }

        if (string.IsNullOrWhiteSpace(deviceId))
        {
            throw new ArgumentException("Value cannot be null or white space", nameof(deviceId));
        }

        var hash = $"{oneTimeToken}.{_config.ApiSecret}.{deviceId}".ToUpperInvariant().ToSha1Hash().ToLowerInvariant();
        return await _httpClient.GetAsync<XummXAppOttResponse>($"xapp/ott/{oneTimeToken}/{hash}");
    }

    /// <inheritdoc />
    public async Task<XummXAppEventResponse> EventAsync(XummXAppEventRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.UserToken))
        {
            throw new ArgumentException("Value cannot be null or white space", nameof(request.UserToken));
        }

        if (string.IsNullOrWhiteSpace(request.Subtitle))
        {
            throw new ArgumentException("Value cannot be null or white space", nameof(request.Subtitle));
        }

        return await _httpClient.PostAsync<XummXAppEventResponse>("xapp/event", request);
    }

    /// <inheritdoc />
    public async Task<XummXAppPushResponse> PushAsync(XummXAppPushRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.UserToken))
        {
            throw new ArgumentException("Value cannot be null or white space", nameof(request.UserToken));
        }

        if (string.IsNullOrWhiteSpace(request.Subtitle))
        {
            throw new ArgumentException("Value cannot be null or white space", nameof(request.Subtitle));
        }

        return await _httpClient.PostAsync<XummXAppPushResponse>("xapp/push", request);
    }
}
