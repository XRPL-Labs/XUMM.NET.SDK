using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Models.Misc
{
    public class XummApplication
    {
        [JsonPropertyName("uuidv4")]
        public string Uuidv4 { get; set; } = default!;

        [JsonPropertyName("name")]
        public string Name { get; set; } = default!;

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("webhookurl")]
        public string? WebhookUrl { get; set; }

        [JsonPropertyName("disabled")]
        public int Disabled { get; set; }

        [JsonPropertyName("icon_url")]
        public string? IconUrl { get; set; }

        [JsonPropertyName("issued_user_token")]
        public object? IssuedUserToken { get; set; }
    }
}
