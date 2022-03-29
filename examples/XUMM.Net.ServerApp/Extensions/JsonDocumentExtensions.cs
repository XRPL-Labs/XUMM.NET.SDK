using System.Text;
using System.Text.Json;

namespace XUMM.NET.ServerApp.Extensions;

public static class JsonDocumentExtensions
{
    public static string? ToJsonString(this JsonDocument? jsonDocument)
    {
        if (jsonDocument == null)
        {
            return null;
        }

        using var stream = new MemoryStream();
        var writer = new Utf8JsonWriter(stream, new JsonWriterOptions
        {
            Indented = true
        });
        jsonDocument.WriteTo(writer);
        writer.Flush();
        return Encoding.UTF8.GetString(stream.ToArray());
    }
}
