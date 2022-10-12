using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace XUMM.NET.SDK.Extensions;

internal static class CryptographyExtensions
{
    internal static string ToSha1Hash(this string value)
    {
        using var sha1Hash = SHA1.Create();
        var hashBytes = sha1Hash.ComputeHash(Encoding.UTF8.GetBytes(value));
        return string.Concat(hashBytes.Select(b => b.ToString("x2")));
    }
}
