using System.Collections.Generic;
using System.Threading.Tasks;
using XUMM.NET.SDK.Enums;
using XUMM.NET.SDK.Models.Misc;

namespace XUMM.NET.SDK.Clients.Interfaces;

/// <summary>
/// Represents the interface of the client for miscellaneous API calls.
/// </summary>
public interface IXummMiscClient
{
    /// <summary>
    /// The ping method allows you to verify API access (valid credentials) and returns some info of your Xumm App.
    /// </summary>
    Task<XummPong> GetPingAsync();

    /// <summary>
    /// Get curated assets from the Xumm API. This API contains the same issuers and assets available to users in Xumm when
    /// they press the "Add asset" button on the home screen.
    /// </summary>
    Task<XummCuratedAssets> GetCuratedAssetsAsync();

    /// <summary>
    /// This method allows you to get the network information for all networks known to Xumm.
    /// </summary>
    Task<List<XummRailsResponse>> GetRailsAsync();

    /// <summary>
    /// Fetch transaction and outcome live from XRP ledger full history nodes (through the Xumm platform) containing parsed
    /// transaction outcome balance mutations.
    /// </summary>
    /// <param name="txHash">The transaction hash (64 hexadecimal characters).</param>
    Task<XummTransaction> GetTransactionAsync(string txHash);

    /// <summary>
    /// Fetch the KYC status for a Xumm user (based on a public XRPL account address, r...).
    /// </summary>
    /// <param name="userTokenOrAccount">Account address, eg. rBLomsmaSJ1ttBmS3WPmPpWLAUDKFwiF9Q</param>
    Task<XummKycStatus> GetKycStatusAsync(string userTokenOrAccount);

    /// <summary>
    /// Get semi-live XRP exchange rates.
    /// </summary>
    /// <param name="currencyCode">The 3 alpha char currency code, eg. INR</param>
    Task<XummRates> GetRatesAsync(string currencyCode);

    /// <summary>
    /// Validate a single user token.
    /// </summary>
    /// <param name="userToken">UUID user token to check and return in the response (validity).</param>
    Task<XummUserTokens> VerifyUserTokenAsync(string userToken);

    /// <summary>
    /// Validate a list of user tokens.
    /// </summary>
    /// <param name="userTokens">List of user token UUID's to check and return in the response (validity).</param>
    Task<XummUserTokens> VerifyUserTokensAsync(string[] userTokens);

    /// <summary>
    /// Fetch the account meta for a Xumm user (based on a public XRPL account address, r...).
    /// </summary>
    /// <param name="account">Account address, eg. rBLomsmaSJ1ttBmS3WPmPpWLAUDKFwiF9Q</param>
    Task<XummAccountMetaResponse> AccountMetaAsync(string account);

    /// <summary>
    /// Get an avatar (gravatar or personal avatar).
    /// </summary>
    /// <param name="account">Account address, eg. rBLomsmaSJ1ttBmS3WPmPpWLAUDKFwiF9Q</param>
    /// <param name="dimensions">Dimensions in pixels.</param>
    /// <param name="padding">Padding surrounding the icon in pixels.</param>
    /// <returns>An avatar with specific (square) dimensions; otherwise a hashicon if no custom avatar can be found.</returns>
    string GetAvatarUrl(string account, int dimensions, int padding);
}
