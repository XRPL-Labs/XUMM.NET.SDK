using System.Text.Json.Serialization;

namespace XUMM.Net.Models
{
    public class XummApiError
    {
        [JsonPropertyName("error")]
        public XummApiErrorDetails Error { get; set; } = default!;
    }
}
