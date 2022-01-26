using System;
using System.Threading;
using System.Threading.Tasks;
using XUMM.Net.EventArgs;
using XUMM.Net.Models.Payload;

namespace XUMM.Net.Clients.Interfaces;

public interface IXummPayloadClient
{
    /// <summary>
    ///     Submit a payload containing a sign request to the XUMM platform.
    /// </summary>
    Task<XummPayloadResponse> SubmitAsync(XummPayload payload);

    /// <summary>
    ///     Get payload details or payload resolve status and result data.
    /// </summary>
    /// <param name="payloadUuid">Payload UUID as received from the Payload POST endpoint.</param>
    Task<XummPayloadDetails> GetAsync(string payloadUuid);

    /// <summary>
    ///     You can get, or wait, for payload status updates using websockets to the xumm API.
    /// </summary>
    /// <param name="payloadUuid">Payload UUID as received from the Payload POST endpoint.</param>
    /// <param name="eventHandler">Event handler to receive subscription messages.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken">CancellationToken</see> to observe.</param>
    /// <returns></returns>
    Task<XummPayloadSubscription?> SubscribeAsync(string payloadUuid,
        EventHandler<XummSubscriptionEventArgs> eventHandler, CancellationToken cancellationToken);
}
