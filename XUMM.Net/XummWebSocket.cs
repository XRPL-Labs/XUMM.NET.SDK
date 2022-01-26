using System;
using System.Collections.Generic;
using System.IO;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XUMM.Net;

public class XummWebSocket : IAsyncDisposable
{
    private readonly Uri _uri;
    private readonly ClientWebSocket _webSocket = new();

    internal XummWebSocket(string uri)
    {
        _uri = new Uri(uri);
    }

    public async ValueTask DisposeAsync()
    {
        await _webSocket.CloseAsync(WebSocketCloseStatus.Empty, string.Empty, CancellationToken.None);
    }

    public async IAsyncEnumerable<string> SubscribeAsync([EnumeratorCancellation] CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        await _webSocket.ConnectAsync(_uri, CancellationToken.None);

        var buffer = new ArraySegment<byte>(new byte[1024]);
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
                break;
            }

            ms.Seek(0, SeekOrigin.Begin);
            yield return Encoding.UTF8.GetString(buffer.Array!, 0, result.Count);
        }
    }
}
