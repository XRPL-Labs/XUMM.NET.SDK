using System;
using System.Security.Cryptography;
using System.Text;

namespace XUMM.NET.SDK.Webhooks;

/// <summary>
/// Verifies the signature Xaman sends with every webhook.
/// </summary>
public static class XummWebhookSignature
{
    /// <summary>
    /// The header with the timestamp that is part of the signed message.
    /// </summary>
    public const string TimestampHeader = "X-Xumm-Request-Timestamp";

    /// <summary>
    /// The header with the lowercase hex HMAC-SHA1 signature.
    /// </summary>
    public const string SignatureHeader = "X-Xumm-Request-Signature";

    // Timestamps and lowercase hex SHA-1 digests are short; anything longer is not a Xaman header.
    private const int MaxHeaderLength = 128;

    /// <summary>
    /// Checks the signature Xaman sends with a webhook: a lowercase hex HMAC-SHA1 over the timestamp header followed by the raw request body,
    /// keyed with the application API secret.
    /// </summary>
    /// <param name="apiSecret">The API secret of the Xaman application.</param>
    /// <param name="timestamp">The value of the <c>X-Xumm-Request-Timestamp</c> header.</param>
    /// <param name="signature">The value of the <c>X-Xumm-Request-Signature</c> header.</param>
    /// <param name="body">The request body exactly as received; a re-serialised copy can differ in escaping and key order.</param>
    /// <returns><c>true</c> when the signature matches.</returns>
    public static bool IsValid(string apiSecret, string? timestamp, string? signature, byte[] body)
    {
        if (string.IsNullOrEmpty(apiSecret) ||
            string.IsNullOrEmpty(timestamp) || timestamp!.Length > MaxHeaderLength ||
            string.IsNullOrEmpty(signature) || signature!.Length > MaxHeaderLength)
        {
            return false;
        }

        var stamp = Encoding.UTF8.GetBytes(timestamp);
        var message = new byte[stamp.Length + body.Length];
        Buffer.BlockCopy(stamp, 0, message, 0, stamp.Length);
        Buffer.BlockCopy(body, 0, message, stamp.Length, body.Length);

        byte[] hash;
        using (var hmac = new HMACSHA1(GetKey(apiSecret)))
        {
            hash = hmac.ComputeHash(message);
        }

        var expected = Encoding.ASCII.GetBytes(ToLowerHex(hash));
        var provided = Encoding.ASCII.GetBytes(signature.Trim().ToLowerInvariant());
        return CryptographicOperations.FixedTimeEquals(expected, provided);
    }

    private static byte[] GetKey(string apiSecret)
    {
        // Xaman's reference sample derives the key with secret.replace('-', ''), which in JavaScript removes only the FIRST dash.
        // Real webhooks verify with exactly that key; removing every dash does not match.
        var firstDash = apiSecret.IndexOf('-');
        return Encoding.UTF8.GetBytes(firstDash < 0 ? apiSecret : apiSecret.Remove(firstDash, 1));
    }

    private static string ToLowerHex(byte[] bytes)
    {
        var builder = new StringBuilder(bytes.Length * 2);
        foreach (var b in bytes)
        {
            builder.Append(b.ToString("x2"));
        }

        return builder.ToString();
    }
}
