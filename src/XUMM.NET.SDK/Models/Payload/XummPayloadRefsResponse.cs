using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Models.Payload;

public class XummPayloadRefsResponse
{
    [JsonPropertyName("qr_png")]
    public string QrPng { get; set; } = default!;

    [JsonPropertyName("qr_matrix")]
    public string QrMatrix { get; set; } = default!;

    [JsonPropertyName("qr_uri_quality_opts")]
    public List<string> QrUriQualityOpts { get; set; } = default!;

    [JsonPropertyName("websocket_status")]
    public string WebsocketStatus { get; set; } = default!;
}