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
        Version = "1.0.1"
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
