using System.Text.Json.Serialization;

namespace XUMM.Net.Models.Misc
{
    public class XummCuratedAssetsDetailsCurrency
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("issuer_id")]
        public int IssuerId { get; set; }

        [JsonPropertyName("issuer")]
        public string Issuer { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("avatar")]
        public string Avatar { get; set; }

        [JsonPropertyName("shortlist")]
        public int Shortlist { get; set; }
    }
}
