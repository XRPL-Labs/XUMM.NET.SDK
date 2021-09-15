using System.Text.Json.Serialization;

namespace XUMM.Net.Models.Misc
{
    public class XummCall
    {
        [JsonPropertyName("uuidv4")]
        public string Uuidv4 { get; set; }
    }
}
