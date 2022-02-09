using System.Text.RegularExpressions;

namespace XUMM.Net.Extensions;

public static class RegularExpressionExtensions
{
    private static readonly Regex RgxAccount = new("^r");
    private static readonly Regex RgxUuid = new(@"^[a-f0-9]{8}\-[a-f0-9]{4}\-[a-f0-9]{4}\-[a-f0-9]{4}\-[a-f0-9]{12}$");

    public static bool IsAccountAddress(this string? input)
    {
        return input != null && RgxAccount.IsMatch(input);
    }

    public static bool IsValidUuid(this string? input)
    {
        return input != null && RgxUuid.IsMatch(input);
    }
}
