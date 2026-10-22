using System.Text;
using NUnit.Framework;

namespace XUMM.NET.SDK.Webhooks.Tests;

[TestFixture]
public class XummWebhookSignatureTests
{
    internal const string ApiSecret = "00000000-1111-2222-3333-444444444444";
    internal const string Timestamp = "1729600000";

    internal const string Body = "{\"meta\":{\"url\":\"https://example.com\",\"application_uuidv4\":\"00000000-1111-2222-3333-444444444444\",\"payload_uuidv4\":\"aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee\",\"opened_by_deeplink\":true},\"custom_meta\":{},\"payloadResponse\":{\"payload_uuidv4\":\"aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee\",\"reference_call_uuidv4\":\"ffffffff-0000-1111-2222-333333333333\",\"signed\":true,\"user_token\":true,\"return_url\":{\"app\":null,\"web\":null},\"txid\":\"ABC\"},\"userToken\":null}";

    // Computed with Xaman's NodeJS sample: createHmac('sha1', secret.replace('-', '')).update(timestamp + body).digest('hex').
    internal const string Signature = "1d54770df27e40ea096f61a3118cb9e03ebe7d5f";

    // The same HMAC keyed with every dash removed, which Xaman does not use.
    private const string AllDashesRemovedSignature = "d58e860acaa025c0ee4324a59137b7c281fb8e1f";

    private static byte[] BodyBytes => Encoding.UTF8.GetBytes(Body);

    [Test]
    public void IsValid_WithXamanSignature_ShouldReturnTrue()
    {
        Assert.That(XummWebhookSignature.IsValid(ApiSecret, Timestamp, Signature, BodyBytes), Is.True);
    }

    [Test]
    public void IsValid_WithUppercaseSignature_ShouldReturnTrue()
    {
        Assert.That(XummWebhookSignature.IsValid(ApiSecret, Timestamp, Signature.ToUpperInvariant(), BodyBytes), Is.True);
    }

    [Test]
    public void IsValid_WithKeyWithoutAnyDashes_ShouldReturnFalse()
    {
        Assert.That(XummWebhookSignature.IsValid(ApiSecret, Timestamp, AllDashesRemovedSignature, BodyBytes), Is.False);
    }

    [Test]
    public void IsValid_WithModifiedBody_ShouldReturnFalse()
    {
        var body = Encoding.UTF8.GetBytes(Body.Replace("\"signed\":true", "\"signed\":false"));

        Assert.That(XummWebhookSignature.IsValid(ApiSecret, Timestamp, Signature, body), Is.False);
    }

    [Test]
    public void IsValid_WithOtherTimestamp_ShouldReturnFalse()
    {
        Assert.That(XummWebhookSignature.IsValid(ApiSecret, "1729600001", Signature, BodyBytes), Is.False);
    }

    [Test]
    public void IsValid_WithOtherSecret_ShouldReturnFalse()
    {
        Assert.That(XummWebhookSignature.IsValid("00000000-1111-2222-3333-555555555555", Timestamp, Signature, BodyBytes), Is.False);
    }

    [TestCase(null, Signature)]
    [TestCase("", Signature)]
    [TestCase(Timestamp, null)]
    [TestCase(Timestamp, "")]
    public void IsValid_WithMissingHeader_ShouldReturnFalse(string? timestamp, string? signature)
    {
        Assert.That(XummWebhookSignature.IsValid(ApiSecret, timestamp, signature, BodyBytes), Is.False);
    }
}
