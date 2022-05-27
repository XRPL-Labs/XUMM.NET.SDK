using System;
using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Models.Misc
{
    public class XummGlobaliD
    {
        [JsonPropertyName("linked")]
        public DateTime Linked { get; set; }

        [JsonPropertyName("profileUrl")]
        public string ProfileUrl { get; set; } = default!;
    }
}
