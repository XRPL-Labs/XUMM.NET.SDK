using System.Text.Json.Serialization;

namespace XUMM.Net.Models.Misc.AppStorage
{
    public class XummStorage
    {
        [JsonPropertyName("application")]
        public XummStorageApplication Application { get; set; } = default!;

        [JsonPropertyName("data")]
        public object? Data { get; set; }
    }
}
