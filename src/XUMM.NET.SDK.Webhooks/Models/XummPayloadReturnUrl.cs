﻿using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Webhooks.Models;

public class XummPayloadReturnUrl
{
    /// <summary>
    /// Smart device application return URL (Optional)
    /// </summary>
    [JsonPropertyName("app")]
    public string? App { get; set; }

    /// <summary>
    /// Web (browser) return URL (Optional)
    /// </summary>
    [JsonPropertyName("web")]
    public string? Web { get; set; }
}
