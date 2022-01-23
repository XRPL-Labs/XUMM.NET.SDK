using System;
using System.Threading.Tasks;

namespace XUMM.Net.Models.Payload
{
    public class XummPayloadSubscription : IAsyncDisposable
    {
        public XummPayloadDetails Payload { get; set; } = default!;
        public XummWebSocket WebSocket { get; set; } = default!;

        public async ValueTask DisposeAsync()
        {
            await WebSocket.DisposeAsync();
        }
    }
}
