using System.Text.RegularExpressions;

namespace XUMM.Net.Extensions
{
    internal static class RegularExpressionExtensions
    {
        private static readonly Regex RgxAccount = new("^r");
        private static readonly Regex RgxUuid = new(@"^[a-f0-9]{8}\-[a-f0-9]{4}\-[a-f0-9]{4}\-[a-f0-9]{4}\-[a-f0-9]{12}$");

        internal static bool IsAccountAddress(this string input)
        {
            return RgxAccount.IsMatch(input);
        }

        internal static bool IsValidUuid(this string input)
        {
            return RgxUuid.IsMatch(input);
        }
    }
}
