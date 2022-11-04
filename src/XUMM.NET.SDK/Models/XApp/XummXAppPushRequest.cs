using System.Text.Json;
using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Models.XApp;

public class XummXAppPushRequest
{
    /// <summary>
    /// The User Token to send the event &amp; push notification to. Obtained with a Sign Request (payload)
    /// </summary>
    [JsonPropertyName("user_token")]
    public string UserToken { get; set; } = default!;

    /// <summary>
    /// Push notification subtitle &amp; subtitle in the Event list (Request tab)
    /// </summary>
    [JsonPropertyName("subtitle")]
    public string? Subtitle { get; set; }

    /// <summary>
    /// Description (text) for the push notification
    /// </summary>
    [JsonPropertyName("body")]
    public string Body { get; set; } = default!;

    /// <summary>
    /// Free form JSON to pass to the Request &amp; push notification context (passed to the JSON received when calling the ott endpoint)
    /// </summary>
    [JsonPropertyName("data")]
    public JsonDocument? Data { get; set; }
}
