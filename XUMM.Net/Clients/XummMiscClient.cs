using System;
using System.Threading.Tasks;
using XUMM.Net.Clients.Interfaces;
using XUMM.Net.Enums;
using XUMM.Net.Extensions;
using XUMM.Net.Helpers;
using XUMM.Net.Models.Misc;

namespace XUMM.Net.Clients;

public class XummMiscClient : IXummMiscClient
{
    private const int MinimumAvatarDimensions = 200;
    private readonly IXummHttpClient _httpClient;

    public XummMiscClient(IXummHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <inheritdoc />
    public async Task<XummPong> PingAsync()
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
        return await _httpClient.GetAsync<XummTransaction>($"xrpl-tx/{txHash}");
    }

    /// <inheritdoc />
    public async Task<XummKycStatus> GetKycStatusAsync(string userTokenOrAccount)
    {
        if (userTokenOrAccount.IsAccountAddress())
        {
            var kycInfo =
                await _httpClient.GetAsync<XummKycInfo>($"kyc-status/{userTokenOrAccount}", true);
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

        return XummKycStatus.None;
    }

    /// <inheritdoc />
    public async Task<XummRates> GetRatesAsync(string currencyCode)
    {
        return await _httpClient.GetAsync<XummRates>($"rates/{currencyCode.Trim().ToUpperInvariant()}");
    }

    /// <inheritdoc />
    public string GetAvatarUrl(string account, int dimensions, int padding)
    {
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
