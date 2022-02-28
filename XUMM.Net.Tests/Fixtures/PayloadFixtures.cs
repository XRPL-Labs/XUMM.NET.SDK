using System.Collections.Generic;
using System.Text.Json;
using XUMM.Net.Models.Payload;

namespace XUMM.Net.Tests.Fixtures;

internal static class PayloadFixtures
{
    internal static XummPayloadResponse XummCreatePayload => new()
    {
        Uuid = "00000000-0000-4839-af2f-f794874a80b0",
        Next = new XummPayloadNextResponse
        {
            Always = "http://localhost:3001/sign/00000000-0000-4839-af2f-f794874a80b0"
        },
        Refs = new XummPayloadRefsResponse
        {
            QrPng = "http://localhost:3001/sign/00000000-0000-4839-af2f-f794874a80b0_q.png",
            QrMatrix = "http://localhost:3001/sign/00000000-0000-4839-af2f-f794874a80b0_q.json",
            QrUriQualityOpts = new List<string> { "m", "q", "h" },
            WebsocketStatus = "ws://localhost:3001/sign/00000000-0000-4839-af2f-f794874a80b0"
        },
        Pushed = false
    };

    internal static string ValidPayloadJson => "{\"TransactionType\":\"Payment\",\"Destination\":\"rPEPPER7kfTD9w2To4CQk6UCfuHM9c6GDY\",\"DestinationTag\":495}";

    internal static string InvalidPayloadJson => "{user_token:'aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee'," +
        "txblob:'1200002400000003614000000002FAF0806840000000000000C873208536F6D65547970657D0" +
        "8536F6D6544617461E1EA7C09446576656C6F7065727D0B4057696574736557696E64E1F1'," +
        "txjson:{TransactionType:'Payment',Destination:'rPEPPER7kfTD9w2To4CQk6UCfuHM9c6GDY',DestinationTag:495,Amount:'65000'}";

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
