using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using XUMM.NET.SDK.Webhooks.Configs;
using XUMM.NET.SDK.Webhooks.Controllers;
using XUMM.NET.SDK.Webhooks.Models;

namespace XUMM.NET.SDK.Webhooks.Tests;

[TestFixture]
public class XummWebhookControllerTests
{
    private Mock<IXummWebhookProcessor> _processorMock = default!;

    [SetUp]
    public void SetUp()
    {
        _processorMock = new Mock<IXummWebhookProcessor>();
    }

    [Test]
    public async Task ProcessAsync_WithoutApiSecret_ShouldProcessUnsignedWebhookAsync()
    {
        // Arrange
        var subject = CreateController(null, null, null);

        // Act
        var result = await subject.ProcessAsync();

        // Assert
        Assert.That(result, Is.InstanceOf<OkResult>());
        _processorMock.Verify(p => p.ProcessAsync(It.IsAny<XummWebhookBody>()), Times.Once);
    }

    [Test]
    public async Task ProcessAsync_WithApiSecretAndValidSignature_ShouldProcessWebhookAsync()
    {
        // Arrange
        var subject = CreateController(XummWebhookSignatureTests.ApiSecret, XummWebhookSignatureTests.Timestamp, XummWebhookSignatureTests.Signature);

        // Act
        var result = await subject.ProcessAsync();

        // Assert
        Assert.That(result, Is.InstanceOf<OkResult>());
        _processorMock.Verify(p => p.ProcessAsync(It.Is<XummWebhookBody>(b => b.PayloadResponse.PayloadUuidv4 == "aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee")), Times.Once);
    }

    [Test]
    public async Task ProcessAsync_WithApiSecretAndInvalidSignature_ShouldRejectWebhookAsync()
    {
        // Arrange
        var subject = CreateController(XummWebhookSignatureTests.ApiSecret, XummWebhookSignatureTests.Timestamp, "0000000000000000000000000000000000000000");

        // Act
        var result = await subject.ProcessAsync();

        // Assert
        Assert.That(result, Is.InstanceOf<UnauthorizedResult>());
        _processorMock.Verify(p => p.ProcessAsync(It.IsAny<XummWebhookBody>()), Times.Never);
    }

    [Test]
    public async Task ProcessAsync_WithApiSecretAndMissingSignature_ShouldRejectWebhookAsync()
    {
        // Arrange
        var subject = CreateController(XummWebhookSignatureTests.ApiSecret, null, null);

        // Act
        var result = await subject.ProcessAsync();

        // Assert
        Assert.That(result, Is.InstanceOf<UnauthorizedResult>());
        _processorMock.Verify(p => p.ProcessAsync(It.IsAny<XummWebhookBody>()), Times.Never);
    }

    private XummWebhookController CreateController(string? apiSecret, string? timestamp, string? signature)
    {
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(XummWebhookSignatureTests.Body));

        if (timestamp != null)
        {
            httpContext.Request.Headers[XummWebhookSignature.TimestampHeader] = timestamp;
        }

        if (signature != null)
        {
            httpContext.Request.Headers[XummWebhookSignature.SignatureHeader] = signature;
        }

        return new XummWebhookController(
            _processorMock.Object,
            Options.Create(new XummWebhookConfig { ApiSecret = apiSecret }),
            new Mock<ILogger<XummWebhookController>>().Object)
        {
            ControllerContext = new ControllerContext { HttpContext = httpContext }
        };
    }
}
