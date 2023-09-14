using System.Text.RegularExpressions;

namespace XUMM.NET.SDK.Extensions;

public static class RegularExpressionExtensions
{
    private static readonly Regex RgxAccount = new(@"^r[1-9A-HJ-NP-Za-km-z]{25,33}$");
    private static readonly Regex RgxSHA512H = new("^[A-Fa-f0-9]{64}$");
    private static readonly Regex RgxUuid = new(@"^[a-f0-9]{8}\-[a-f0-9]{4}\-[a-f0-9]{4}\-[a-f0-9]{4}\-[a-f0-9]{12}$");

    public static bool IsAccountAddress(this string? input)
    {
        return input != null && RgxAccount.IsMatch(input);
    }

    /// <summary>
    /// Many objects in the XRP Ledger, particularly transactions and ledgers, are uniquely identified by a 256-bit hash value.
    /// This value is typically calculated as a "SHA-512Half", which calculates a SHA-512  hash from some contents, then takes the first half of the output.
    /// (That's 256 bits, which is 32 bytes, or 64 characters of the hexadecimal representation.)
    /// </summary>
    public static bool IsSHA512H(this string? input)
    {
        return input != null && RgxSHA512H.IsMatch(input);
    }

    public static bool IsValidUuid(this string? input)
    {
        return input != null && RgxUuid.IsMatch(input);
    }
}
