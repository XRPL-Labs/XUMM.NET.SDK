using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace XUMM.NET.SDK.Extensions;

public static class CurrencyExtensions
{
    private static readonly Regex RgxCurrencyHex = new("^[a-fA-F0-9]{40}$");
    private static readonly Regex RgxDecodedHex = new("[a-zA-Z0-9]{3,}");
    private static readonly string HexReplacementPattern = "(00)+$";

    /// <summary>
    /// Format currency to standard currency code
    /// </summary>
    /// <param name="currency">
    /// Standard Currency Codes: As a 3-character string such as "EUR" or "USD" or
    /// Nonstandard Currency Codes: As a 160-bit hexadecimal string, such as "0158415500000000C1F76FF6ECB0BAC600000000".
    /// </param>
    /// <param name="maxLength">Maximum length of decoded HEX currency code.</param>
    /// <returns>Returns string representation of a Standard Currency Codes</returns>
    public static string ToFormattedCurrency(this string currency, int maxLength = 12)
    {
        currency = currency.Trim();
        if (currency.Length == 3 && !currency.ToUpper().Equals("XRP"))
        {
            return currency;
        }

        if (RgxCurrencyHex.IsMatch(currency))
        {
            var hex = Regex.Replace(currency, HexReplacementPattern, string.Empty);
            var bytes = Enumerable.Range(0, hex.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                .ToArray();

            if (hex.StartsWith("02"))
            {
                bytes = bytes.Skip(8).ToArray();
            }

            var decoded = Encoding.UTF8.GetString(bytes);
            if (decoded.Length > maxLength)
            {
                decoded = decoded[..maxLength];
            }

            if (RgxDecodedHex.IsMatch(decoded) && !currency.ToUpper().Equals("XRP"))
            {
                return decoded;
            }
        }

        return "???";
    }
}
