using System;
using System.Collections.Generic;
using System.Text.Json;
using XUMM.Net.Models.Misc;
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

    internal static XummPayloadDetails XummPayloadDetails => new()
    {
        Meta = new XummPayloadDetailsMeta
        {
            Exists = true,
            Uuid = "00000000-0000-4839-af2f-f794874a80b0",
            Multisign = false,
            Submit = false,
            Destination = string.Empty,
            ResolvedDestination = string.Empty,
            Resolved = true,
            Signed = true,
            Cancelled = false,
            Expired = true,
            Pushed = false,
            AppOpened = true,
            OpenedByDeeplink = true,
            ReturnUrlApp = "https://xumm.dev/beta/test?payloadId=00000000-0000-4839-af2f-f794874a80b0&customIdent=&txid=9B124C14528ED14C0BDA17075A39B90ABED598B77A22DFEEBD913CAC07A513BC&hex=1200032280000000240000003241833237B8665D2F4E00135E8DE646589F68400000000000000C732103709723A5967EAAED571B71DB511D87FA44CC7CDDF827A37F457A25E14D862BCD74473045022100C6A6999BD33153C6A236D78438D1BFEEEC810CFE05D0E41339B577560C9143CA022074F07881F559F56593FF680049C12FC3BCBB0B73CE02338651522891D95886F981146078086881F39B191D63B528D914FEA7F8CA2293F9EA7C06636C69656E747D15426974686F6D7020746F6F6C20762E20302E302E337E0A706C61696E2F74657874E1F1",
            ReturnUrlWeb = "https://xumm.dev/beta/test?payloadId=00000000-0000-4839-af2f-f794874a80b0&customIdent=&txid=9B124C14528ED14C0BDA17075A39B90ABED598B77A22DFEEBD913CAC07A513BC&hex=1200032280000000240000003241833237B8665D2F4E00135E8DE646589F68400000000000000C732103709723A5967EAAED571B71DB511D87FA44CC7CDDF827A37F457A25E14D862BCD74473045022100C6A6999BD33153C6A236D78438D1BFEEEC810CFE05D0E41339B577560C9143CA022074F07881F559F56593FF680049C12FC3BCBB0B73CE02338651522891D95886F981146078086881F39B191D63B528D914FEA7F8CA2293F9EA7C06636C69656E747D15426974686F6D7020746F6F6C20762E20302E302E337E0A706C61696E2F74657874E1F1",
            IsXapp = false
        },
        Application = new XummApplication
        {
            Name = "SomeApp",
            Description = "SomeDesc",
            Disabled = 0,
            Uuidv4 = "00000000-1111-2222-af2f-f794874a80b0",
            IconUrl = "https://xumm-cdn.imgix.net/app-logo/00000000-1111-2222-9abc-bf0a7cf9f5cc.png",
            IssuedUserToken = null
        },
        Payload = new XummPayloadDetailsPayloadResponse
        {
            TxType = "SignIn",
            TxDestination = string.Empty,
            TxDestinationTag = null,
            RequestJson = JsonDocument.Parse("{\"TransactionType\":\"SignIn\",\"SignIn\":true}"),
            OriginType = "QR",
            SignMethod = "TANGEM",
            CreatedAt = new DateTime(2020, 05, 03, 19, 28, 21, DateTimeKind.Utc),
            ExpiresAt = new DateTime(2020, 05, 03, 19, 38, 21, DateTimeKind.Utc),
            ExpiresInSeconds = -11557255
        },
        Response = new XummPayloadDetailsResponse
        {
            Hex = "ABCDEF123456789",
            Txid = "ABCDEF123456789",
            ResolvedAt = new DateTime(2020, 05, 03, 17, 28, 40, DateTimeKind.Utc),
            DispatchedTo = "wss://xrplcluster.com",
            DispatchedResult = "tes_SUCCESS",
            DispatchedNodetype = "MAINNET",
            MultisignAccount = "rPEPPER7kfTD9w2To4CQk6UCfuHM9c6GDY",
            Account = "rPEPPER7kfTD9w2To4CQk6UCfuHM9c6GDY"
        },
        CustomMeta = new XummPayloadCustomMeta
        {
            Identifier = null,
            Blob = JsonDocument.Parse("{\"country\":\"Netherlands\"}"),
            Instruction = "Hey ❤️! Please sign for\n\nThis\nThat 🍻"
        }
    };

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
