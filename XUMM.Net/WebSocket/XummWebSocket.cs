using System;
using System.Collections.Generic;
using System.IO;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace XUMM.Net.WebSocket;

public class XummWebSocket : IAsyncDisposable
{
    private readonly ILogger? _logger;
    private readonly string _payloadUuid;
    private readonly Uri _uri;
    private readonly ClientWebSocket _webSocket = new();

    internal XummWebSocket(string payloadUuid, ILogger? logger)
    {
        _payloadUuid = payloadUuid;
        _uri = new Uri($"wss://xumm.app/sign/{_payloadUuid}");
        _logger = logger;
    }

    public async ValueTask DisposeAsync()
    {
        await _webSocket.CloseAsync(WebSocketCloseStatus.Empty, string.Empty, CancellationToken.None);
        WriteLog("Subscription ended (WebSocket closed).");
    }

    public async IAsyncEnumerable<string> SubscribeAsync([EnumeratorCancellation] CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        await _webSocket.ConnectAsync(_uri, CancellationToken.None);

        if (_webSocket.State == WebSocketState.Open)
        {
            var buffer = new ArraySegment<byte>(new byte[1024]);

            WriteLog("Subscription active (WebSocket opened).");

            while (_webSocket.State == WebSocketState.Open)
            {
                await using var ms = new MemoryStream();
                WebSocketReceiveResult? result;

                try
                {
                    do
                    {
                        result = await _webSocket.ReceiveAsync(buffer, cancellationToken);
                        ms.Write(buffer.Array!, buffer.Offset, result.Count);
                    } while (!result.EndOfMessage && !cancellationToken.IsCancellationRequested);
                }
                catch (OperationCanceledException)
                {
                    WriteLog("Subscription ended (WebSocket closed).");
                    yield break;
                }

                ms.Seek(0, SeekOrigin.Begin);
                yield return Encoding.UTF8.GetString(buffer.Array!, 0, result.Count);
            }
        }
    }

    private void WriteLog(string logMessage)
    {
        _logger?.LogInformation("Payload {0}: {1}", _payloadUuid, logMessage);
    }
}
