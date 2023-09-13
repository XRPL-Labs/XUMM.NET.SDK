using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XUMM.NET.SDK.Clients.Interfaces;
using XUMM.NET.SDK.Enums;
using XUMM.NET.SDK.Extensions;
using XUMM.NET.SDK.Helpers;
using XUMM.NET.SDK.Models.Misc;

namespace XUMM.NET.SDK.Clients;

/// <summary>
/// Represents the client for miscellaneous API calls.
/// </summary>
public class XummMiscClient : IXummMiscClient
{
    private const int MinimumAvatarDimensions = 200;
    private readonly IXummHttpClient _httpClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="XummMiscClient"/> class.
    /// </summary>
    public XummMiscClient(IXummHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <inheritdoc />
    public async Task<XummPong> GetPingAsync()
    {
        return await _httpClient.GetAsync<XummPong>("platform/ping");
    }

    /// <inheritdoc />
    public async Task<XummCuratedAssets> GetCuratedAssetsAsync()
    {
        return await _httpClient.GetAsync<XummCuratedAssets>("platform/curated-assets");
    }

    /// <inheritdoc />
    public async Task<XummHookInfoResponse> GetHookInfoAsync(string hookHash)
    {
        if (!hookHash.IsSHA512H())
        {
            throw new ArgumentException("Invalid Hook Hash (expecting SHA-512Half)", nameof(hookHash));
        }

        var result = await _httpClient.GetAsync<XummHookInfo>($"platform/hookhash/{hookHash}");
        return new XummHookInfoResponse(hookHash, result);
    }

    /// <inheritdoc />
    public async Task<List<XummHookInfoResponse>> GetAllHookInfosAsync()
    {
        var result = await _httpClient.GetAsync<Dictionary<string, XummHookInfo>>("platform/hookhash");
        return result.Select(x => new XummHookInfoResponse(x.Key, x.Value)).ToList();
    }

    /// <inheritdoc />
    public async Task<XummTransaction> GetTransactionAsync(string txHash)
    {
        if (!txHash.IsSHA512H())
        {
            throw new ArgumentException("Invalid Transaction Hash (expecting SHA-512Half)", nameof(txHash));
        }

        return await _httpClient.GetAsync<XummTransaction>($"platform/xrpl-tx/{txHash}");
    }

    /// <inheritdoc />
    public async Task<XummKycStatus> GetKycStatusAsync(string userTokenOrAccount)
    {
        if (string.IsNullOrWhiteSpace(userTokenOrAccount))
        {
            throw new ArgumentException("Value cannot be null or white space", nameof(userTokenOrAccount));
        }

        if (userTokenOrAccount.IsAccountAddress())
        {
            var kycInfo = await _httpClient.GetPublicAsync<XummKycInfo>($"platform/kyc-status/{userTokenOrAccount}");
            return kycInfo.KycApproved ? XummKycStatus.Successful : XummKycStatus.None;
        }

        if (userTokenOrAccount.IsValidUuid())
        {
            var request = new XummKycStatusRequest
            {
                UserToken = userTokenOrAccount
            };

            var kycInfo = await _httpClient.PostAsync<XummKycStatusInfo>("platform/kyc-status", request);
            return EnumHelper.GetValueFromName<XummKycStatus>(kycInfo.KycStatus);
        }

        throw new ArgumentException("Invalid user token or account provided", nameof(userTokenOrAccount));
    }

    /// <inheritdoc />
    public async Task<XummRates> GetRatesAsync(string currencyCode)
    {
        if (string.IsNullOrWhiteSpace(currencyCode))
        {
            throw new ArgumentException("Value cannot be null or white space", nameof(currencyCode));
        }

        return await _httpClient.GetAsync<XummRates>($"platform/rates/{currencyCode.Trim().ToUpperInvariant()}");
    }

    /// <inheritdoc />
    public async Task<XummUserTokens> VerifyUserTokenAsync(string userToken)
    {
        if (string.IsNullOrWhiteSpace(userToken))
        {
            throw new ArgumentException("Value cannot be null or white space", nameof(userToken));
        }

        return await _httpClient.GetAsync<XummUserTokens>($"platform/user-token/{userToken}");
    }

    /// <inheritdoc />
    public async Task<XummUserTokens> VerifyUserTokensAsync(string[] userTokens)
    {
        if (userTokens == null || userTokens.Length == 0)
        {
            throw new ArgumentException("Value cannot be null or empty", nameof(userTokens));
        }

        var request = new XummUserTokensRequest
        {
            Tokens = new List<string>(userTokens)
        };

        return await _httpClient.PostAsync<XummUserTokens>("platform/user-tokens", request);
    }

    /// <inheritdoc />
    public async Task<XummAccountMetaResponse> AccountMetaAsync(string account)
    {
        if (!account.IsAccountAddress())
        {
            throw new ArgumentException("Value should be a valid account address", nameof(account));
        }

        return await _httpClient.GetAsync<XummAccountMetaResponse>($"platform/account-meta/{account}");
    }

    /// <inheritdoc />
    public string GetAvatarUrl(string account, int dimensions, int padding)
    {
        if (string.IsNullOrWhiteSpace(account))
        {
            throw new ArgumentException("Value cannot be null or white space", nameof(account));
        }

        if (dimensions < MinimumAvatarDimensions)
        {
            throw new ArgumentOutOfRangeException(nameof(dimensions),
                $"The minimum (square) dimensions are {MinimumAvatarDimensions}.");
        }

        if (padding < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(padding), "The padding should be equal or greater than zero.");
        }

        return $"https://xumm.app/avatar/{account}_{dimensions}_{padding}.png";
    }
}
