using System.Text.Json.Serialization;

namespace XUMM.Net.Models.Misc
{
    public class XummApplication
    {
        [JsonPropertyName("uuidv4")]
        public string Uuidv4 { get; set; } = default!;

        [JsonPropertyName("name")]
        public string Name { get; set; } = default!;

        [JsonPropertyName("webhookurl")]
        public string WebhookUrl { get; set; } = default!;

        [JsonPropertyName("disabled")]
        public int Disabled { get; set; }
    }
}
