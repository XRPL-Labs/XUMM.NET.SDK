using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Models.Payload;

public class XummPayloadOptions
{
    /// <summary>
    /// Should the xumm app submit to the XRPL after signing? (Optional)
    /// </summary>
    [JsonPropertyName("submit")]
    public bool? Submit { get; set; }

    /// <summary>
    /// Should the transaction be signed as a multi sign transaction? (Optional)
    /// </summary>
    [JsonPropertyName("multisign")]
    public bool? MultiSign { get; set; }

    /// <summary>
    /// After how many minutes should the payload expire? (Optional)
    /// </summary>
    [JsonPropertyName("expire")]
    public int Expire { get; set; }

    /// <summary>
    /// Force any of the provided accounts to sign. (Optional)
    /// </summary>
    [JsonPropertyName("signers")]
    public string[]? Signers { get; set; }

    /// <summary>
    /// Where should the user be redirected to after resolving the payload? (Optional)
    /// </summary>
    [JsonPropertyName("return_url")]
    public XummPayloadReturnUrl? ReturnUrl { get; set; }
}
