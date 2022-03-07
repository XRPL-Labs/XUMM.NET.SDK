using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Models.Payload
{
    public class XummPayloadDetailsPayloadResponse
    {
        [JsonPropertyName("tx_type")]
        public string TxType { get; set; } = default!;

        [JsonPropertyName("tx_destination")]
        public string TxDestination { get; set; } = default!;

        [JsonPropertyName("tx_destination_tag")]
        public int? TxDestinationTag { get; set; }

        [JsonPropertyName("request_json")]
        public JsonDocument RequestJson { get; set; } = default!;

        [JsonPropertyName("origintype")]
        public string? OriginType { get; set; }

        [JsonPropertyName("signmethod")]
        public string? SignMethod { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("expires_at")]
        public DateTime ExpiresAt { get; set; }

        [JsonPropertyName("expires_in_seconds")]
        public int ExpiresInSeconds { get; set; }
    }
}
