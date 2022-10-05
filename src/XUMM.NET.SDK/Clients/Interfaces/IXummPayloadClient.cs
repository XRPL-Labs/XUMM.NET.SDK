using System;
using System.Threading;
using System.Threading.Tasks;
using XUMM.NET.SDK.Models.Payload;
using XUMM.NET.SDK.Models.Payload.Xumm;
using XUMM.NET.SDK.WebSocket.EventArgs;

namespace XUMM.NET.SDK.Clients.Interfaces;

/// <summary>
/// Represents the interface of the client for Payload API calls.
/// </summary>
public interface IXummPayloadClient
{
    /// <summary>
    /// Submit a payload containing a sign request to the Xumm platform.
    /// </summary>
    /// <param name="payload">Payload to create.</param>
    /// <param name="throwError">Throws an exception if an error occurred; otherwise errors are ignored.</param>
    Task<XummPayloadResponse?> CreateAsync(XummPostJsonPayload payload, bool throwError = false);

    /// <summary>
    /// Submit a payload containing a sign request to the Xumm platform.
    /// </summary>
    /// <param name="payload">Payload to create.</param>
    /// <param name="throwError">Throws an exception if an error occurred; otherwise errors are ignored.</param>
    Task<XummPayloadResponse?> CreateAsync(XummPostBlobPayload payload, bool throwError = false);

    /// <summary>
    /// Submit a payload containing a sign request to the Xumm platform.
    /// </summary>
    /// <param name="payloadTransaction">Payload to create.</param>
    /// <param name="throwError">Throws an exception if an error occurred; otherwise errors are ignored.</param>
    Task<XummPayloadResponse?> CreateAsync(XummPayloadTransaction payloadTransaction, bool throwError = false);

    /// <summary>
    /// Get payload details or payload resolve status and result data.
    /// </summary>
    /// <param name="payload">The <see cref="XummPayloadResponse" /> return value of :
    ///     <list type="table">
    ///         <item><see cref="CreateAsync(XummPostJsonPayload, bool)" /></item>
    ///         <item><see cref="CreateAsync(XummPostBlobPayload, bool)" /></item>
    ///         <item><see cref="CreateAsync(XummPayloadTransaction, bool)" /></item>
    ///     </list>
    /// </param>
    /// <param name="throwError">Throws an exception if an error occurred; otherwise errors are ignored.</param>
    Task<XummPayloadDetails?> GetAsync(XummPayloadResponse payload, bool throwError = false);

    /// <summary>
    /// Get payload details or payload resolve status and result data.
    /// </summary>
    /// <param name="payloadUuid">Payload UUID as received from the Payload POST endpoint.</param>
    /// <param name="throwError">Throws an exception if an error occurred; otherwise errors are ignored.</param>
    Task<XummPayloadDetails?> GetAsync(string payloadUuid, bool throwError = false);

    /// <summary>
    /// Get payload details or payload resolve status and result data by custom identifier.
    /// </summary>
    /// <param name="customIdentifier">
    /// Custom payload identifier as provided when posting your payload to the Payload POST
    /// endpoint (<see cref="XummPayloadCustomMeta.Identifier" />).
    /// </param>
    /// <param name="throwError">Throws an exception if an error occurred; otherwise errors are ignored.</param>
    Task<XummPayloadDetails?> GetByCustomIdentifierAsync(string customIdentifier, bool throwError = false);

    /// <summary>
    /// Cancel a payload, so a user cannot open it anymore
    /// </summary>
    /// <param name="payloadResponse">The <see cref="XummPayloadResponse" /> return value of:
    ///     <list type="table">
    ///         <item><see cref="CreateAsync(XummPostJsonPayload, bool)" /></item>
    ///         <item><see cref="CreateAsync(XummPostBlobPayload, bool)" /></item>
    ///         <item><see cref="CreateAsync(XummPayloadTransaction, bool)" /></item>
    ///     </list>
    /// </param>
    /// <param name="throwError">Throws an exception if an error occurred; otherwise errors are ignored.</param>
    Task<XummDeletePayload?> CancelAsync(XummPayloadResponse payloadResponse, bool throwError = false);

    /// <summary>
    /// Cancel a payload, so a user cannot open it anymore
    /// </summary>
    /// <param name="payloadDetails">The <see cref="XummPayloadDetails" /> return value of:
    ///     <list type="table">
    ///         <item><see cref="GetAsync(XummPayloadResponse, bool)" /></item>
    ///         <item><see cref="GetAsync(string, bool)" /></item>
    ///     </list>
    /// </param>
    /// <param name="throwError">Throws an exception if an error occurred; otherwise errors are ignored.</param>
    Task<XummDeletePayload?> CancelAsync(XummPayloadDetails payloadDetails, bool throwError = false);

    /// <summary>
    /// Cancel a payload, so a user cannot open it anymore
    /// </summary>
    /// <param name="payloadUuid">Payload UUID as received from the Payload POST endpoint.</param>
    /// <param name="throwError">Throws an exception if an error occurred; otherwise errors are ignored.</param>
    Task<XummDeletePayload?> CancelAsync(string payloadUuid, bool throwError = false);

    /// <summary>
    /// You can get, or wait, for payload status updates using websockets to the Xumm API.
    /// </summary>
    /// <param name="payload">The <see cref="XummPayloadDetails" /> return value of:
    ///     <list type="table">
    ///         <item><see cref="GetAsync(XummPayloadResponse, bool)" /></item>
    ///         <item><see cref="GetAsync(string, bool)" /></item>
    ///     </list>
    /// </param>
    /// <param name="eventHandler">Event handler to receive subscription messages.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken">CancellationToken</see> to observe.</param>
    /// <returns></returns>
    Task SubscribeAsync(XummPayloadDetails payload, EventHandler<XummSubscriptionEventArgs> eventHandler, CancellationToken cancellationToken);

    /// <summary>
    /// You can get, or wait, for payload status updates using websockets to the Xumm API.
    /// </summary>
    /// <param name="payload">The <see cref="XummPayloadResponse" /> return value of:
    ///     <list type="table">
    ///         <item><see cref="CreateAsync(XummPostJsonPayload, bool)" /></item>
    ///         <item><see cref="CreateAsync(XummPostBlobPayload, bool)" /></item>
    ///         <item><see cref="CreateAsync(XummPayloadTransaction, bool)" /></item>
    ///     </list>
    /// </param>
    /// <param name="eventHandler">Event handler to receive subscription messages.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken">CancellationToken</see> to observe.</param>
    /// <returns></returns>
    Task SubscribeAsync(XummPayloadResponse payload, EventHandler<XummSubscriptionEventArgs> eventHandler, CancellationToken cancellationToken);

    /// <summary>
    /// You can get, or wait, for payload status updates using websockets to the Xumm API.
    /// </summary>
    /// <param name="payloadUuid">Payload UUID as received from the Payload POST endpoint.</param>
    /// <param name="eventHandler">Event handler to receive subscription messages.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken">CancellationToken</see> to observe.</param>
    /// <returns></returns>
    Task SubscribeAsync(string payloadUuid, EventHandler<XummSubscriptionEventArgs> eventHandler, CancellationToken cancellationToken);

    /// <summary>
    /// You can get, or wait, for payload status updates using websockets to the Xumm API.
    /// </summary>
    /// <param name="payload">Payload to create.</param>
    /// <param name="eventHandler">Event handler to receive subscription messages.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken">CancellationToken</see> to observe.</param>
    /// <returns></returns>
    Task<XummPayloadResponse> CreateAndSubscribeAsync(XummPostJsonPayload payload, EventHandler<XummSubscriptionEventArgs> eventHandler, CancellationToken cancellationToken);

    /// <summary>
    /// You can get, or wait, for payload status updates using websockets to the Xumm API.
    /// </summary>
    /// <param name="payload">Payload to create.</param>
    /// <param name="eventHandler">Event handler to receive subscription messages.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken">CancellationToken</see> to observe.</param>
    /// <returns></returns>
    Task<XummPayloadResponse> CreateAndSubscribeAsync(XummPostBlobPayload payload, EventHandler<XummSubscriptionEventArgs> eventHandler, CancellationToken cancellationToken);
}
