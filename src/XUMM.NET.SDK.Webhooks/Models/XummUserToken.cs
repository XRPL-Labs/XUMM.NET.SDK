using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Webhooks.Models;

public class XummUserToken
{
    [JsonPropertyName("user_token")]
    public string UserToken { get; set; } = default!;

    [JsonPropertyName("token_issued")]
    public int TokenIssued { get; set; }

    [JsonPropertyName("token_expiration")]
    public int TokenExpiration { get; set; }
}
