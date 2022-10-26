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
}
