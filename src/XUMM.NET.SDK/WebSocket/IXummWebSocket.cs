using System.Collections.Generic;
using System.Threading;

namespace XUMM.NET.SDK.WebSocket;

public interface IXummWebSocket
{
    IAsyncEnumerable<string> SubscribeAsync(string payloadUuid, CancellationToken cancellationToken);
}