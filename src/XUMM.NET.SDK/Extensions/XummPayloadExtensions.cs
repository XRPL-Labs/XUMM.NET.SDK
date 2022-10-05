using System.Text.Json;
using XUMM.NET.SDK.Helpers;
using XUMM.NET.SDK.Models.Payload;

namespace XUMM.NET.SDK.Extensions;

public static class XummPayloadExtensions
{
    /// <summary>
    /// Serialize the <paramref name="payloadObject"/> as the <see cref="XummPostJsonPayload.TxJson"/> of <see cref="XummPostJsonPayload"/>.
    /// </summary>
    public static XummPostJsonPayload ToXummPostJsonPayload(this object payloadObject)
    {
        var json = JsonSerializer.Serialize(payloadObject, JsonHelper.SerializerOptions);
        return new XummPostJsonPayload(json);
    }
}