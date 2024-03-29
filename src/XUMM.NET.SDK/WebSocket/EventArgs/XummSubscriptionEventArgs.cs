﻿using System;
using System.Text.Json;
using XUMM.NET.SDK.Models.Payload;

namespace XUMM.NET.SDK.WebSocket.EventArgs;

public class XummSubscriptionEventArgs
{
    public string Uuid { get; set; } = default!;
    public JsonDocument Data { get; set; } = default!;
    public XummPayloadDetails Payload { get; set; } = default!;
    public Action CloseConnectionAsync { get; set; } = default!;
}
