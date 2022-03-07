using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Models.Misc
{
    internal class XummKycStatusRequest
    {
        [JsonPropertyName("user_token")]
        public string UserToken { get; set; } = default!;
    }
}
