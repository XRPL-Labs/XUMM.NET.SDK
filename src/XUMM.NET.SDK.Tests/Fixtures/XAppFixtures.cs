using System.Text.Json;
using XUMM.NET.SDK.Models.XApp;

namespace XUMM.NET.SDK.Tests.Fixtures;

internal static class XAppFixtures
{
    internal static XummXAppOttResponse XummXAppOtt => new()
    {
        Account = "r...",
        AccountAccess = "FULL",
        AccountType = "REGULAR",
        Locale = "en",
        Style = "LIGHT",
        Version = "1.0.1",
        Origin = new XummXAppOriginResponse
        {
            Type = "Test",
            Data = new XummXAppOriginDataResponse
            {
                AdditionalData = new System.Collections.Generic.Dictionary<string, JsonElement>
                {
                    ["data_string"] = JsonDocument.Parse("\"Test string\"").RootElement,
                    ["data_empty_string"] = JsonDocument.Parse("\"\"").RootElement,
                    ["data_null_string"] = JsonDocument.Parse("null").RootElement,
                    ["data_number_int32"] = JsonDocument.Parse("2147483647").RootElement,
                    ["data_number_int64"] = JsonDocument.Parse("9223372036854775807").RootElement,
                    ["data_number_negative"] = JsonDocument.Parse("-123").RootElement,
                }
            }
        }
    };

    internal static XummXAppEventResponse EventResponse => new()
    {
        Pushed = true,
        Uuid = "19dd8a5b-e167-49a1-8f21-50f0254c55ef"
    };

    internal static XummXAppPushResponse PushResponse => new()
    {
        Pushed = true
    };
}
