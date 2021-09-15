using System.Text.Json.Serialization;

namespace XUMM.Net.Models.Misc
{
    public class XummPong
    {
        [JsonPropertyName("pong")]
        public bool Pong { get; set; }

        [JsonPropertyName("auth")]
        public XummAuth Auth { get; set; } = default!;
    }
}
