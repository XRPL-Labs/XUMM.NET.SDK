using System;
using System.Threading.Tasks;
using XUMM.NET.SDK.Clients.Interfaces;
using XUMM.NET.SDK.Models.XAppJwt;

namespace XUMM.NET.SDK.Clients;

public class XummXAppJwtClient : IXummXAppJwtClient
{
    private readonly IXummHttpClient _xummHttpClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="XummXAppJwtClient"/> class.
    /// </summary>
    public XummXAppJwtClient(IXummHttpClient xummhttpClient)
    {
        _xummHttpClient = xummhttpClient;
    }

    /// <inheritdoc />
    public async Task<XummXAppJwtAuthorizeResponse> AuthorizeAsync(string oneTimeToken)
    {
        if (string.IsNullOrWhiteSpace(oneTimeToken))
        {
            throw new ArgumentException("Value cannot be null or white space", nameof(oneTimeToken));
        }

        var httpClient = _xummHttpClient.GetHttpClient(true);
        httpClient.DefaultRequestHeaders.Add("X-API-OTT", oneTimeToken);
        return await _xummHttpClient.GetAsync<XummXAppJwtAuthorizeResponse>(httpClient, "xapp-jwt/authorize");
    }

    /// <inheritdoc />
    public async Task<XummXAppJwtUserDataResponse> GetUserDataAsync(string jwt, string key)
    {
        var httpClient = _xummHttpClient.GetHttpClient(false);
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {jwt}");

        return await _xummHttpClient.GetAsync<XummXAppJwtUserDataResponse>(httpClient, $"xapp-jwt/userdata/{key}");
    }

    /// <inheritdoc />
    public async Task<XummXAppJwtUserDataUpdateResponse> SetUserDataAsync(string jwt, string key, string json)
    {
        var httpClient = _xummHttpClient.GetHttpClient(false);
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {jwt}");

        return await _xummHttpClient.PostAsync<XummXAppJwtUserDataUpdateResponse>(httpClient, $"xapp-jwt/userdata/{key}", json);
    }

    /// <inheritdoc />
    public async Task<XummXAppJwtUserDataUpdateResponse> DeleteUserDataAsync(string jwt, string key)
    {
        var httpClient = _xummHttpClient.GetHttpClient(false);
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {jwt}");

        return await _xummHttpClient.DeleteAsync<XummXAppJwtUserDataUpdateResponse>(httpClient, $"xapp-jwt/userdata/{key}");
    }

    /// <inheritdoc />
    public async Task<XummXAppJwtNFTokenDetail> GetNFTokenDetailAsync(string jwt, string tokenId)
    {
        if (string.IsNullOrWhiteSpace(tokenId))
        {
            throw new ArgumentException("Value cannot be null or white space", nameof(tokenId));
        }

        var httpClient = _xummHttpClient.GetHttpClient(false);
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {jwt}");

        return await _xummHttpClient.GetAsync<XummXAppJwtNFTokenDetail>(httpClient, $"xapp-jwt/nftoken-detail/{tokenId}");
    }
}
