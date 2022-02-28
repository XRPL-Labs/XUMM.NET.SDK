using System.Threading.Tasks;
using XUMM.Net.Enums;
using XUMM.Net.Models.Misc;

namespace XUMM.Net.Clients.Interfaces;

public interface IXummMiscClient
{
    Task<XummPong> GetPingAsync();

    /// <summary>
    /// Get curated assets from the XUMM API. This API contains the same issuers and assets available to users in XUMM when
    /// they press the "Add asset" button on the home screen.
    /// </summary>
    Task<XummCuratedAssets> GetCuratedAssetsAsync();

    /// <summary>
    /// Fetch transaction & outcome live from XRP ledger full history nodes (through the XUMM platform) containing parsed
    /// transaction outcome balance mutations
    /// </summary>
    /// <param name="txHash">The transaction hash (64 hexadecimal characters)</param>
    Task<XummTransaction> GetTransactionAsync(string txHash);

    /// <summary>
    /// Fetch the KYC status for a XUMM user (based on a public XRPL account address, r...)
    /// </summary>
    /// <param name="userTokenOrAccount">Account address, eg. rDWLGshgAxSX2G4TEv3gA6QhtLgiXrWQXB</param>
    Task<XummKycStatus> GetKycStatusAsync(string userTokenOrAccount);

    /// <summary>
    /// Get semi-live XRP exchange rates
    /// </summary>
    /// <param name="currencyCode">The 3 alpha char currency code, eg. INR</param>
    Task<XummRates> GetRatesAsync(string currencyCode);

    /// <summary>
    /// Get an avatar (gravatar or personal avatar)
    /// </summary>
    /// <param name="account">Account address, eg. rDWLGshgAxSX2G4TEv3gA6QhtLgiXrWQXB</param>
    /// <param name="dimensions">Dimensions in pixels</param>
    /// <param name="padding">Padding surrounding the icon in pixels</param>
    /// <returns>An avatar with specific (square) dimensions; otherwise a hashicon if no custom avatar can be found.</returns>
    string GetAvatarUrl(string account, int dimensions, int padding);
}
