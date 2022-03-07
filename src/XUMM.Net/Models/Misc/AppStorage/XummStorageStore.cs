using System.Text.Json.Serialization;

namespace XUMM.Net.Models.Misc.AppStorage
{
    public class XummStorageStore : XummStorage
    {
        [JsonPropertyName("stored")]
        public bool Stored { get; set; }
    }
}
