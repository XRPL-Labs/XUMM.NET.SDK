using System;
using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Models.Payload;

public class XummPayloadDetailsResponse
{
    [JsonPropertyName("hex")]
    public string? Hex { get; set; }

    [JsonPropertyName("txid")]
    public string? Txid { get; set; }

    [JsonPropertyName("resolved_at")]
    public DateTime? ResolvedAt { get; set; }

    [JsonPropertyName("dispatched_to")]
    public string? DispatchedTo { get; set; }

    [JsonPropertyName("dispatched_result")]
    public string? DispatchedResult { get; set; }

    [JsonPropertyName("dispatched_nodetype")]
    public string? DispatchedNodetype { get; set; }

    [JsonPropertyName("multisign_account")]
    public string? MultisignAccount { get; set; }

    [JsonPropertyName("account")]
    public string? Account { get; set; }
}