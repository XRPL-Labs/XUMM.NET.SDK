using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Models.XApp;

public class XummXAppEventRequest : XummXAppPushRequest
{
    /// <summary>
    /// Only create the event in the user's Event list, don't send a push notification
    /// </summary>
    [JsonPropertyName("silent")]
    public bool Silent { get; set; }
}
