using System;
using System.Text.Json;
using XUMM.Net.Models.Payload;

namespace XUMM.Net.EventArgs
{
    public class XummSubscriptionEventArgs
    {
        public string Uuid { get; set; } = default!;
        public JsonDocument Data { get; set; } = default!;
        public XummPayloadDetails Payload { get; set; } = default!;
        public Action CloseConnectionAsync { get; set; } = default!;
    }
}
