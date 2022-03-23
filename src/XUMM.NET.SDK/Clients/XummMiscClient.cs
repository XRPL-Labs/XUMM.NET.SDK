using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XUMM.NET.SDK.Clients.Interfaces;
using XUMM.NET.SDK.Enums;
using XUMM.NET.SDK.Extensions;
using XUMM.NET.SDK.Helpers;
using XUMM.NET.SDK.Models.Misc;

namespace XUMM.NET.SDK.Clients;

public class XummMiscClient : IXummMiscClient
{
    private const int MinimumAvatarDimensions = 200;
    private readonly IXummHttpClient _httpClient;

    public XummMiscClient(IXummHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <inheritdoc />
    public async Task<XummPong> GetPingAsync()
    {
        return await _httpClient.GetAsync<XummPong>("ping");
    }

    /// <inheritdoc />
    public async Task<XummCuratedAssets> GetCuratedAssetsAsync()
    {
        return await _httpClient.GetAsync<XummCuratedAssets>("curated-assets");
    }

    /// <inheritdoc />
    public async Task<XummTransaction> GetTransactionAsync(string txHash)
    {
        if (string.IsNullOrWhiteSpace(txHash))
        {
            throw new ArgumentException("Value cannot be null or white space", nameof(txHash));
        }

        return await _httpClient.GetAsync<XummTransaction>($"xrpl-tx/{txHash}");
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
            var kycInfo = await _httpClient.GetPublicAsync<XummKycInfo>($"kyc-status/{userTokenOrAccount}");
            return kycInfo.KycApproved ? XummKycStatus.Successful : XummKycStatus.None;
        }

        if (userTokenOrAccount.IsValidUuid())
        {
            var request = new XummKycStatusRequest
            {
                UserToken = userTokenOrAccount
            };

            var kycInfo = await _httpClient.PostAsync<XummKycStatusInfo>("kyc-status", request);
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

        return await _httpClient.GetAsync<XummRates>($"rates/{currencyCode.Trim().ToUpperInvariant()}");
    }

    /// <inheritdoc />
    public async Task<XummUserTokens> VerifyUserTokenAsync(string userToken)
    {
        if (string.IsNullOrWhiteSpace(userToken))
        {
            throw new ArgumentException("Value cannot be null or white space", nameof(userToken));
        }

        return await _httpClient.GetAsync<XummUserTokens>($"user-token/{userToken}");
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

        return await _httpClient.PostAsync<XummUserTokens>("user-tokens", request);
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
