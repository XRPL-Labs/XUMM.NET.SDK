using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Models.Payload;

public class XummPostBlobPayload : XummPayloadBodyBase
{
    public XummPostBlobPayload(string txBlob)
    {
        TxBlob = txBlob;
    }

    /// <summary>
    /// You can provide a HEX transaction template instead of a JSON formatted <see cref="XummPostJsonPayload"/>.
    /// </summary>
    [JsonPropertyName("txblob")]
    public string? TxBlob { get; }
}