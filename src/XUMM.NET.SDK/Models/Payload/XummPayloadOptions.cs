using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Models.Payload;

public class XummPayloadOptions
{
    /// <summary>
    /// (Optional) Should the xumm app submit to the XRPL after signing? 
    /// </summary>
    [JsonPropertyName("submit")]
    public bool? Submit { get; set; }

    /// <summary>
    /// (Optional) Allow pathfinding for regular Payment type transactions, so the user can select the asset to send to deliver the requested asset amount.
    /// </summary>
    [JsonPropertyName("pathfinding")]
    public bool? Pathfinding { get; set; }

    /// <summary>
    /// (Optional) Allow Xumm clients &lt; version 2.4.0 to fall back from modern pathfinding UX to a native 1:1 asset payment.
    /// </summary>
    [JsonPropertyName("pathfinding_fallback")]
    public bool? PathfindingFallback { get; set; }

    /// <summary>
    /// (Optional) Should the transaction be signed as a multi sign transaction?
    /// </summary>
    [JsonPropertyName("multisign")]
    public bool? MultiSign { get; set; }

    /// <summary>
    /// (Optional) After how many minutes should the payload expire?
    /// </summary>
    [JsonPropertyName("expire")]
    public int Expire { get; set; }

    /// <summary>
    /// (Optional) Force any of the provided accounts to sign.
    /// </summary>
    [JsonPropertyName("signers")]
    public string[]? Signers { get; set; }

    /// <summary>
    /// (Optional) When set, the payload can only be opened by a device connected to the specified network. Xumm 2.5.0 and higher required.
    /// </summary>
    [JsonPropertyName("force_network")]
    public string? ForceNetwork { get; set; }

    /// <summary>
    /// (Optional) Where should the user be redirected to after resolving the payload?
    /// </summary>
    [JsonPropertyName("return_url")]
    public XummPayloadReturnUrl? ReturnUrl { get; set; }
}
