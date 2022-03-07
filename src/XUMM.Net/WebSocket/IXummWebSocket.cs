using System.Collections.Generic;
using System.Threading;

namespace XUMM.Net.WebSocket
{
    public interface IXummWebSocket
    {
        IAsyncEnumerable<string> SubscribeAsync(string payloadUuid, CancellationToken cancellationToken);
    }
}
