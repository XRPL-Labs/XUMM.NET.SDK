using System.Threading.Tasks;
using XUMM.NET.SDK.Enums;
using XUMM.NET.SDK.Models.Misc;

namespace XUMM.NET.SDK.Clients.Interfaces;

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
    /// Validate a User Token
    /// </summary>
    /// <param name="userToken">UUID User Token to check and return in the response (validity)</param>
    Task<XummUserTokens> VerifyUserTokenAsync(string userToken);

    /// <summary>
    /// Validate a list of User Tokens
    /// </summary>
    /// <param name="userTokens">List of User Token UUID's to check and return in the response (validity)</param>
    Task<XummUserTokens> VerifyUserTokensAsync(string[] userTokens);

    /// <summary>
    /// Get an avatar (gravatar or personal avatar)
    /// </summary>
    /// <param name="account">Account address, eg. rDWLGshgAxSX2G4TEv3gA6QhtLgiXrWQXB</param>
    /// <param name="dimensions">Dimensions in pixels</param>
    /// <param name="padding">Padding surrounding the icon in pixels</param>
    /// <returns>An avatar with specific (square) dimensions; otherwise a hashicon if no custom avatar can be found.</returns>
    string GetAvatarUrl(string account, int dimensions, int padding);
}
