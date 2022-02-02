using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using XUMM.Net.Clients.Interfaces;
using XUMM.Net.Models.Payload;
using XUMM.Net.WebSocket;
using XUMM.Net.WebSocket.EventArgs;

namespace XUMM.Net.Clients;

public class XummPayloadClient : IXummPayloadClient
{
    private readonly IXummHttpClient _httpClient;
    private readonly ILogger<IXummPayloadClient> _logger;

    public XummPayloadClient(IXummHttpClient httpClient,
        ILogger<IXummPayloadClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<XummPayloadResponse?> CreateAsync(XummPayload payload, bool throwError = false)
    {
        try
        {
            return await _httpClient.PostAsync<XummPayloadResponse>("payload", payload);
        }
        catch
        {
            if (!throwError)
            {
                return default;
            }

            throw;
        }
    }

    /// <inheritdoc />
    public async Task<XummPayloadDetails?> GetAsync(string payloadUuid, bool throwError = false)
    {
        try
        {
            return await _httpClient.GetAsync<XummPayloadDetails>($"payload/{payloadUuid}");
        }
        catch
        {
            if (!throwError)
            {
                return default;
            }

            throw;
        }
    }

    /// <inheritdoc />
    public async Task<XummDeletePayload?> CancelAsync(string payloadUuid, bool throwError = false)
    {
        try
        {
            return await _httpClient.DeleteAsync<XummDeletePayload>($"payload/{payloadUuid}");
        }
        catch
        {
            if (!throwError)
            {
                return default;
            }

            throw;
        }
    }

    /// <inheritdoc />
    public async Task<XummPayloadSubscription> SubscribeAsync(XummPayloadDetails payload,
        EventHandler<XummSubscriptionEventArgs> eventHandler,
        CancellationToken cancellationToken)
    {
        return await SubscribeAsync(payload.Meta.Uuid, eventHandler, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<XummPayloadSubscription> SubscribeAsync(XummPayloadResponse payload,
        EventHandler<XummSubscriptionEventArgs> eventHandler,
        CancellationToken cancellationToken)
    {
        return await SubscribeAsync(payload.Uuid, eventHandler, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<XummPayloadSubscription> SubscribeAsync(string payloadUuid,
        EventHandler<XummSubscriptionEventArgs> eventHandler,
        CancellationToken cancellationToken)
    {
        var source = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

        //This is ugly, but there's a small chance a created XUMM payload has not been distributed
        //across the load balanced XUMM backend, so wait a bit.
        await Task.Delay(75, cancellationToken);

        var payload = await GetAsync(payloadUuid);
        if (payload != null)
        {
            var webSocket = new XummWebSocket(payloadUuid, _logger);
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
