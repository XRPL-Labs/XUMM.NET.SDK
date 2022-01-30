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
    private readonly XummClient _xummClient;

    internal XummMiscClient(XummClient xummClient)
    {
        AppStorage = new XummMiscAppStorageClient(xummClient);
        _xummClient = xummClient;
    }

    /// <inheritdoc />
    public IXummMiscAppStorageClient AppStorage { get; }

    /// <inheritdoc />
    public async Task<XummPong> PingAsync()
    {
        return await _xummClient.GetAsync<XummPong>("ping");
    }

    /// <inheritdoc />
    public async Task<XummCuratedAssets> GetCuratedAssetsAsync()
    {
        return await _xummClient.GetAsync<XummCuratedAssets>("curated-assets");
    }

    /// <inheritdoc />
    public async Task<XummTransaction> GetTransactionAsync(string txHash)
    {
        return await _xummClient.GetAsync<XummTransaction>($"xrpl-tx/{txHash}");
    }

    /// <inheritdoc />
    public async Task<XummKycStatus> GetKycStatusAsync(string userTokenOrAccount)
    {
        if (userTokenOrAccount.IsAccountAddress())
        {
            var kycInfo =
                await _xummClient.GetAsync<XummKycInfo>($"kyc-status/{userTokenOrAccount}", true);
            return kycInfo.KycApproved ? XummKycStatus.Successful : XummKycStatus.None;
        }
        else
        {
            var request = new XummKycStatusRequest
            {
                UserToken = userTokenOrAccount
            };
            var kycInfo = await _xummClient.PostAsync<XummKycStatusInfo>("kyc-status", request);
            return EnumHelper.GetValueFromName<XummKycStatus>(kycInfo.KycStatus);
        }
    }

    /// <inheritdoc />
    public async Task<XummRates> GetRatesAsync(string currencyCode)
    {
        return await _xummClient.GetAsync<XummRates>($"rates/{currencyCode.Trim().ToUpperInvariant()}");
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
