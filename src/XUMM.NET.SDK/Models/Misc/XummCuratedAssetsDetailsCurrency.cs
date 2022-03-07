using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Models.Misc
{
    public class XummCuratedAssetsDetailsCurrency
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("issuer_id")]
        public int IssuerId { get; set; }

        [JsonPropertyName("issuer")]
        public string Issuer { get; set; } = default!;

        [JsonPropertyName("currency")]
        public string Currency { get; set; } = default!;

        [JsonPropertyName("name")]
        public string Name { get; set; } = default!;

        [JsonPropertyName("avatar")]
        public string Avatar { get; set; } = default!;

        [JsonPropertyName("shortlist")]
        public int Shortlist { get; set; }
    }
}
