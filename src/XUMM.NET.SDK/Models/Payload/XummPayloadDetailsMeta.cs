using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Models.Payload
{
    public class XummPayloadDetailsMeta
    {
        [JsonPropertyName("exists")]
        public bool Exists { get; set; }

        [JsonPropertyName("uuid")]
        public string Uuid { get; set; } = default!;

        [JsonPropertyName("multisign")]
        public bool Multisign { get; set; }

        [JsonPropertyName("submit")]
        public bool Submit { get; set; }

        [JsonPropertyName("destination")]
        public string Destination { get; set; } = default!;

        [JsonPropertyName("resolved_destination")]
        public string ResolvedDestination { get; set; } = default!;

        [JsonPropertyName("resolved")]
        public bool Resolved { get; set; }

        [JsonPropertyName("signed")]
        public bool Signed { get; set; }

        [JsonPropertyName("cancelled")]
        public bool Cancelled { get; set; }

        [JsonPropertyName("expired")]
        public bool Expired { get; set; }

        [JsonPropertyName("pushed")]
        public bool Pushed { get; set; }

        [JsonPropertyName("app_opened")]
        public bool AppOpened { get; set; }

        [JsonPropertyName("opened_by_deeplink")]
        public bool? OpenedByDeeplink { get; set; }

        [JsonPropertyName("openedimmutable_by_deeplink")]
        public bool? Immutable { get; set; }

        [JsonPropertyName("return_url_app")]
        public string? ReturnUrlApp { get; set; }

        [JsonPropertyName("return_url_web")]
        public string? ReturnUrlWeb { get; set; }

        [JsonPropertyName("is_xapp")]
        public bool IsXapp { get; set; }
    }
}
