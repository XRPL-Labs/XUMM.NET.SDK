using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using XUMM.Net.Clients.Interfaces;
using XUMM.Net.EventArgs;
using XUMM.Net.Models.Payload;

namespace XUMM.Net.Clients;

public class XummPayloadClient : IXummPayloadClient
{
    private readonly XummClient _xummClient;

    internal XummPayloadClient(XummClient xummClient)
    {
        _xummClient = xummClient;
    }

    /// <inheritdoc />
    public async Task<XummPayloadResponse?> CreateAsync(XummPayload payload, bool throwError = false)
    {
        return await _xummClient.PostAsync<XummPayloadResponse>("payload", payload, throwError);
    }

    /// <inheritdoc />
    public async Task<XummPayloadDetails?> GetAsync(string payloadUuid, bool throwError = false)
    {
        return await _xummClient.GetAsync<XummPayloadDetails>($"payload/{payloadUuid}", throwError);
    }

    /// <inheritdoc />
    public async Task<XummDeletePayload?> CancelAsync(string payloadUuid, bool throwError = false)
    {
        return await _xummClient.DeleteAsync<XummDeletePayload>($"payload/{payloadUuid}", throwError);
    }

    /// <inheritdoc />
    public async Task<XummPayloadSubscription> SubscribeAsync(string payloadUuid,
        EventHandler<XummSubscriptionEventArgs> eventHandler,
        CancellationToken cancellationToken)
    {
        var source = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

        var payload = await _xummClient.Payload.GetAsync(payloadUuid);
        if (payload != null)
        {
            var webSocket = new XummWebSocket($"wss://xumm.app/sign/{payloadUuid}");
            await foreach (var message in webSocket.SubscribeAsync(source.Token))
            {
                eventHandler(this,
                    new XummSubscriptionEventArgs
                    {
                        Uuid = payloadUuid,
                        Payload = payload,
                        Data = JsonDocument.Parse(message),
                        CloseConnectionAsync = () => source.Cancel()
                    });
            }
        }

        return new XummPayloadSubscription
        {
            Payload = payload
        };
    }

    public async Task<XummPayloadSubscription> CreateAndSubscribeAsync(XummPayload payload,
        EventHandler<XummSubscriptionEventArgs> eventHandler,
        CancellationToken cancellationToken)
    {
        var createdPayload = await CreateAsync(payload);
        if (createdPayload == null)
        {
            throw new Exception("Error creating payload or subscribing to created payload");
        }

        return await SubscribeAsync(createdPayload.Uuid, eventHandler, cancellationToken);
    }
}
