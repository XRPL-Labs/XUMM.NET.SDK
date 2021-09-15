using System.Text.Json.Serialization;

namespace XUMM.Net.Models.Misc
{
    public class XummApplication
    {
        [JsonPropertyName("uuidv4")]
        public string Uuidv4 { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("webhookurl")]
        public string WebhookUrl { get; set; }

        [JsonPropertyName("disabled")]
        public int Disabled { get; set; }
    }
}
