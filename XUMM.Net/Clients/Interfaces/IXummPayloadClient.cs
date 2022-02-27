using System;
using System.Threading;
using System.Threading.Tasks;
using XUMM.Net.Models.Payload;
using XUMM.Net.WebSocket.EventArgs;

namespace XUMM.Net.Clients.Interfaces;

public interface IXummPayloadClient
{
    /// <summary>
    /// Submit a payload containing a sign request to the XUMM platform.
    /// </summary>
    /// <param name="payload">Payload to create.</param>
    /// <param name="throwError">Throws an exception if an error occurred; otherwise errors are ignored..</param>
    Task<XummPayloadResponse?> CreateAsync(XummPayload payload, bool throwError = false);

    /// <summary>
    /// Get payload details or payload resolve status and result data.
    /// </summary>
    /// <param name="payloadUuid">Payload UUID as received from the Payload POST endpoint.</param>
    /// <param name="throwError">Throws an exception if an error occurred; otherwise errors are ignored..</param>
    Task<XummPayloadDetails?> GetAsync(string payloadUuid, bool throwError = false);

    /// <summary>
    /// Cancel a payload, so a user cannot open it anymore
    /// </summary>
    /// <param name="payloadResponse">The <see cref="XummPayloadResponse" /> return value of <see cref="CreateAsync" />.</param>
    /// <param name="throwError">Throws an exception if an error occurred; otherwise errors are ignored..</param>
    Task<XummDeletePayload?> CancelAsync(XummPayloadResponse payloadResponse, bool throwError = false);

    /// <summary>
    /// Cancel a payload, so a user cannot open it anymore
    /// </summary>
    /// <param name="payloadDetails">The <see cref="XummPayloadDetails" /> return value of <see cref="GetAsync" />.</param>
    /// <param name="throwError">Throws an exception if an error occurred; otherwise errors are ignored..</param>
    Task<XummDeletePayload?> CancelAsync(XummPayloadDetails payloadDetails, bool throwError = false);

    /// <summary>
    /// Cancel a payload, so a user cannot open it anymore
    /// </summary>
    /// <param name="payloadUuid">Payload UUID as received from the Payload POST endpoint.</param>
    /// <param name="throwError">Throws an exception if an error occurred; otherwise errors are ignored..</param>
    Task<XummDeletePayload?> CancelAsync(string payloadUuid, bool throwError = false);

    /// <summary>
    /// You can get, or wait, for payload status updates using websockets to the xumm API.
    /// </summary>
    /// <param name="payload">The <see cref="XummPayloadDetails" /> return value of <see cref="GetAsync" />.</param>
    /// <param name="eventHandler">Event handler to receive subscription messages.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken">CancellationToken</see> to observe.</param>
    /// <returns></returns>
    Task<XummPayloadSubscription> SubscribeAsync(XummPayloadDetails payload,
        EventHandler<XummSubscriptionEventArgs> eventHandler, CancellationToken cancellationToken);

    /// <summary>
    /// You can get, or wait, for payload status updates using websockets to the xumm API.
    /// </summary>
    /// <param name="payload">The <see cref="XummPayloadResponse" /> return value of <see cref="CreateAsync" />.</param>
    /// <param name="eventHandler">Event handler to receive subscription messages.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken">CancellationToken</see> to observe.</param>
    /// <returns></returns>
    Task<XummPayloadSubscription> SubscribeAsync(XummPayloadResponse payload,
        EventHandler<XummSubscriptionEventArgs> eventHandler, CancellationToken cancellationToken);

    /// <summary>
    /// You can get, or wait, for payload status updates using websockets to the xumm API.
    /// </summary>
    /// <param name="payloadUuid">Payload UUID as received from the Payload POST endpoint.</param>
    /// <param name="eventHandler">Event handler to receive subscription messages.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken">CancellationToken</see> to observe.</param>
    /// <returns></returns>
    Task<XummPayloadSubscription> SubscribeAsync(string payloadUuid,
        EventHandler<XummSubscriptionEventArgs> eventHandler, CancellationToken cancellationToken);

    /// <summary>
    /// You can get, or wait, for payload status updates using websockets to the xumm API.
    /// </summary>
    /// <param name="payload">Payload to create.</param>
    /// <param name="eventHandler">Event handler to receive subscription messages.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken">CancellationToken</see> to observe.</param>
    /// <returns></returns>
    Task<XummPayloadSubscription> CreateAndSubscribeAsync(XummPayload payload,
        EventHandler<XummSubscriptionEventArgs> eventHandler, CancellationToken cancellationToken);
}
