namespace XUMM.NET.SDK.Webhooks.Configs;

/// <summary>
/// Configuration of the Xaman webhook endpoint.
/// </summary>
public class XummWebhookConfig
{
    /// <summary>
    /// (Optional) The API secret of the Xaman application that sends the webhooks.
    /// When set, <see cref="Controllers.XummWebhookController"/> verifies the <c>X-Xumm-Request-Signature</c> header of every webhook
    /// and rejects the request with 401 Unauthorized when it is missing or does not match.
    /// When not set, webhooks are passed to the processor without verification.
    /// </summary>
    public string? ApiSecret { get; set; }
}
