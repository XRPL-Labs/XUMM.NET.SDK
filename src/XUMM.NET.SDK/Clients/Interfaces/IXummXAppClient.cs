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

    /// <summary>
    /// The 'event' endpoint allows publishing an xApp event in the "Requests" event list of a user, while sending a Push notification pointing to the event.
    /// </summary>
    /// <param name="request">Event request values.</param>
    Task<XummXAppEventResponse> EventAsync(XummXAppEventRequest request);

    /// <summary>
    /// The 'push' endpoint allows publishing a push notification linking to an xApp. If the user clears the push notification there is no way to retrieve the link to the xApp.
    /// </summary>
    /// <param name="request">Push request values.</param>
    Task<XummXAppPushResponse> PushAsync(XummXAppPushRequest request);
}
