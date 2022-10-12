using System.Threading.Tasks;
using XUMM.NET.SDK.Models.XApp;

namespace XUMM.NET.SDK.Clients.Interfaces;

/// <summary>
/// Represents the interface of the client for xApp API calls.
/// </summary>
public interface IXummXAppClient
{
    /// <summary>
    /// This 'ott' (one time token) endpoint allows the xApp backend to retrieve verified session related information from the Xumm user.
    /// xApps are embedded apps.Publishing and xApp and calling xApp API endpoints are only available for XRPL-Labs / Xumm partners.
    /// </summary>
    /// <param name="oneTimeToken">UUID (token) received (URL get param.) when Xumm launches your xApp URL.</param>
    Task<XummXAppOttResponse> GetOneTimeTokenDataAsync(string oneTimeToken);

    /// <summary>
    /// During xApp development, you will most likely test in your (desktop) browser, appending the OTT information (and style param.) you retrieved after initially calling your xApp from Xumm.
    /// </summary>
    /// <param name="oneTimeToken">UUID (token) received (URL get param.) when Xumm launches your xApp URL.</param>
    /// <param name="deviceId">The device ID that retrieved the One Time Token data.</param>
    Task<XummXAppOttResponse> ReFetchOneTimeTokenDataAsync(string oneTimeToken, string deviceId);
}
