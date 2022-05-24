using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Webhooks.Models;

public class XummPayloadResponse
{
    [JsonPropertyName("payload_uuidv4")]
    public string PayloadUuidv4 { get; set; } = default!;

    [JsonPropertyName("reference_call_uuidv4")]
    public string ReferenceCallUuidv4 { get; set; } = default!;

    [JsonPropertyName("signed")]
    public bool Signed { get; set; }

    [JsonPropertyName("user_token")]
    public bool UserToken { get; set; }

    /// <summary>
    /// Where should the user be redirected to after resolving the payload? (Optional)
    /// </summary>
    [JsonPropertyName("return_url")]
    public XummPayloadReturnUrl? ReturnUrl { get; set; }

    [JsonPropertyName("txid")]
    public string TxId { get; set; } = default!;
}
