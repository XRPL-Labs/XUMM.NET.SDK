using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Models.Misc.AppStorage
{
    public class XummStorageStore : XummStorage
    {
        [JsonPropertyName("stored")]
        public bool Stored { get; set; }
    }
}
