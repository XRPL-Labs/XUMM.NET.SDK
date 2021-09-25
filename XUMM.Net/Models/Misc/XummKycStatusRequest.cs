using System.Text.Json.Serialization;

namespace XUMM.Net.Models.Misc
{
    internal class XummKycStatusRequest
    {
        [JsonPropertyName("user_token")]
        public string UserToken { get; set; } = default!;
    }
}
