using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using XUMM.NET.SDK.Clients.Interfaces;
using XUMM.NET.SDK.Models.Payload;
using XUMM.NET.SDK.Models.Payload.Xumm;
using XUMM.NET.SDK.WebSocket;
using XUMM.NET.SDK.WebSocket.EventArgs;

namespace XUMM.NET.SDK.Clients;

public class XummPayloadClient : IXummPayloadClient
{
    private readonly IXummHttpClient _httpClient;
    private readonly IXummWebSocket _webSocket;

    public XummPayloadClient(IXummHttpClient httpClient,
        IXummWebSocket webSocket)
    {
        _httpClient = httpClient;
        _webSocket = webSocket;
    }

    /// <inheritdoc />
    public async Task<XummPayloadResponse?> CreateAsync(XummPostJsonPayload payload, bool throwError = false)
    {
        return await CreatePayloadAsync(payload, throwError);
    }

    /// <inheritdoc />
    public async Task<XummPayloadResponse?> CreateAsync(XummPostBlobPayload payload, bool throwError = false)
    {
        return await CreatePayloadAsync(payload, throwError);
    }

    /// <inheritdoc />
    public async Task<XummPayloadResponse?> CreateAsync(XummPayloadTransaction payloadTransaction, bool throwError = false)
    {
        try
        {
            return await _httpClient.PostAsync<XummPayloadResponse>("payload", new Dictionary<string, object> { { "txJson", payloadTransaction } });
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

    private async Task<XummPayloadResponse?> CreatePayloadAsync(XummPayloadBodyBase payload, bool throwError = false)
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
    public async Task<XummPayloadDetails?> GetAsync(XummPayloadResponse payload, bool throwError = false)
    {
        return await GetAsync(payload.Uuid, throwError);
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
    public async Task<XummDeletePayload?> CancelAsync(XummPayloadResponse payloadResponse, bool throwError = false)
    {
        return await CancelAsync(payloadResponse.Uuid, throwError);
    }

    /// <inheritdoc />
    public async Task<XummDeletePayload?> CancelAsync(XummPayloadDetails payloadDetails, bool throwError = false)
    {
        return await CancelAsync(payloadDetails.Meta.Uuid, throwError);
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
    public async Task SubscribeAsync(XummPayloadDetails payload,
        EventHandler<XummSubscriptionEventArgs> eventHandler,
        CancellationToken cancellationToken)
    {
        await SubscribeAsync(payload.Meta.Uuid, eventHandler, cancellationToken);
    }

    /// <inheritdoc />
    public async Task SubscribeAsync(XummPayloadResponse payload,
        EventHandler<XummSubscriptionEventArgs> eventHandler,
        CancellationToken cancellationToken)
    {
        await SubscribeAsync(payload.Uuid, eventHandler, cancellationToken);
    }

    /// <inheritdoc />
    public async Task SubscribeAsync(string payloadUuid,
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
            await foreach (var message in _webSocket.SubscribeAsync(payloadUuid, source.Token))
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
    }

    public async Task<XummPayloadResponse> CreateAndSubscribeAsync(XummPostJsonPayload payload,
        EventHandler<XummSubscriptionEventArgs> eventHandler,
        CancellationToken cancellationToken)
    {
        return await CreateAndSubscribePayloadAsync(payload, eventHandler, cancellationToken);
    }

    public async Task<XummPayloadResponse> CreateAndSubscribeAsync(XummPostBlobPayload payload,
        EventHandler<XummSubscriptionEventArgs> eventHandler,
        CancellationToken cancellationToken)
    {
        return await CreateAndSubscribePayloadAsync(payload, eventHandler, cancellationToken);
    }

    private async Task<XummPayloadResponse> CreateAndSubscribePayloadAsync(XummPayloadBodyBase payload,
        EventHandler<XummSubscriptionEventArgs> eventHandler,
        CancellationToken cancellationToken)
    {
        var result = await CreatePayloadAsync(payload, true);
        await SubscribeAsync(result!.Uuid, eventHandler, cancellationToken);
        return result;
    }
}
