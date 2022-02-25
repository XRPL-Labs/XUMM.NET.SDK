using System.Text.Json;
using XUMM.Net.Models.Payload;

namespace XUMM.Net.Tests.Fixtures;

internal static class PayloadFixtures
{
    internal static XummDeletePayload XummDeletePayload => new()
    {
        Result = new XummDeletePayloadResult
        {
            Cancelled = true,
            Reason = "OK"
        },
        Meta = new XummPayloadDetailsMeta
        {
            Exists = true,
            Uuid = "00000000-0000-4839-af2f-f794874a80b0",
            Multisign = false,
            Submit = false,
            Destination = string.Empty,
            ResolvedDestination = string.Empty,
            Resolved = false,
            Signed = false,
            Cancelled = false,
            Expired = true,
            Pushed = false,
            AppOpened = false,
            OpenedByDeeplink = false,
            ReturnUrlApp = "https://xumm.dev/beta/test?payloadId=00000000-0000-4839-af2f-f794874a80b0&customIdent=&txid=&hex=",
            ReturnUrlWeb = "https://xumm.dev/beta/test?payloadId=00000000-0000-4839-af2f-f794874a80b0&customIdent=&txid=&hex=",
            IsXapp = false
        },
        CustomMeta = new XummPayloadCustomMeta
        {
            Identifier = null,
            Blob = JsonDocument.Parse("{\"location\":\"Amersfoort\"}"),
            Instruction = null
        }
    };
}
