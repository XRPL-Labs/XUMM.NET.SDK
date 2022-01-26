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
    public async Task<XummPayloadResponse> CreateAsync(XummPayload payload)
    {
        return await _xummClient.PostAsync<XummPayloadResponse>("payload", payload);
    }

    /// <inheritdoc />
    public async Task<XummPayloadDetails> GetAsync(string payloadUuid)
    {
        return await _xummClient.GetAsync<XummPayloadDetails>($"payload/{payloadUuid}");
    }

    /// <inheritdoc />
    public async Task<XummDeletePayload> CancelAsync(string payloadUuid)
    {
        return await _xummClient.DeleteAsync<XummDeletePayload>($"payload/{payloadUuid}");
    }

    /// <inheritdoc />
    public async Task<XummPayloadSubscription?> SubscribeAsync(string payloadUuid,
        EventHandler<XummSubscriptionEventArgs> eventHandler,
        CancellationToken cancellationToken)
    {
        XummPayloadSubscription? result = null;
        var payload = await _xummClient.Payload.GetAsync(payloadUuid);

        if (payload != null)
        {
            var source = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            result = new XummPayloadSubscription
            {
                Payload = payload, WebSocket = new XummWebSocket($"wss://xumm.app/sign/{payloadUuid}")
            };

            await foreach (var message in result.WebSocket.SubscribeAsync(source.Token))
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

        return result;
    }
}
