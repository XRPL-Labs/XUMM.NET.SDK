using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Models.XApp;

public class XummXAppOttResponse
{
    [JsonPropertyName("locale")]
    public string? Locale { get; set; }

    [JsonPropertyName("version")]
    public string? Version { get; set; }

    [JsonPropertyName("account")]
    public string? Account { get; set; }

    [JsonPropertyName("accountaccess")]
    public string? AccountAccess { get; set; }

    [JsonPropertyName("accounttype")]
    public string? AccountType { get; set; }

    [JsonPropertyName("style")]
    public string? Style { get; set; }

    [JsonPropertyName("origin")]
    public XummXAppOriginResponse? Origin { get; set; }

    [JsonPropertyName("user")]
    public string User { get; set; } = default!;

    [JsonPropertyName("user_device")]
    public XummXAppUserDeviceDataResponse? UserDevice { get; set; }

    [JsonPropertyName("account_info")]
    public XummXAppAccountInfoResponse AccountInfo { get; set; } = default!;

    [JsonPropertyName("nodetype")]
    public string? NodeType { get; set; }

    [JsonPropertyName("nodewss")]
    public string? NodeWebSocketSecure { get; set; }

    [JsonPropertyName("currency")]
    public string? Currency { get; set; }

    [JsonPropertyName("subscriptions")]
    public string[]? Subscriptions { get; set; }
}