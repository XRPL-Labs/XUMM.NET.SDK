using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using XUMM.NET.SDK.Clients;
using XUMM.NET.SDK.Configs;
using XUMM.NET.SDK.Tests.Extensions;
using XUMM.NET.SDK.Tests.Fixtures;

namespace XUMM.NET.SDK.Tests.Clients;

[TestFixture]
public class XummXAppClientTests
{
    private Mock<XummHttpClient> _xummHttpClient = default!;
    private Mock<HttpMessageHandler> _httpMessageHandlerMock = default!;
    private Mock<IHttpClientFactory> _httpClientFactory = default!;
    private XummXAppClient _subject = default!;

    [SetUp]
    public void SetUp()
    {
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>();

        _httpClientFactory = new Mock<IHttpClientFactory>();
        _httpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>()))
            .Returns(new HttpClient(_httpMessageHandlerMock.Object));

        var options = Options.Create(new ApiConfig
        {
            ApiKey = "00000000-0000-0000-0000-000000000000",
            ApiSecret = "00000000-0000-0000-0000-000000000000"
        });

        _xummHttpClient = new Mock<XummHttpClient>(
            _httpClientFactory.Object,
            options,
            new Mock<ILogger<XummHttpClient>>().Object);

        _subject = new XummXAppClient(_xummHttpClient.Object, options);
    }

    [Test]
    [TestCase("1b9105dd-b7e7-456b-8303-3b9c7e48a622")]
    public async Task GetOneTimeTokenDataAsync_WithValidOneTimeToken_ShouldReturnOneTimeTokenDataAsync(string oneTimeToken)
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.OK, "xapp-get");

        // Act
        var result = await _subject.GetOneTimeTokenDataAsync(oneTimeToken);

        // Assert
        AssertExtensions.AreEqual(XAppFixtures.XummXAppOtt, result);
    }

    [Test]
    [TestCase("1b9105dd-b7e7-456b-8303-3b9c7e48a622")]
    [TestCase("f0460610-0283-42fe-b246-fb0c20a3eda4")]
    public async Task GetOneTimeTokenDataAsync_WithValidOneTimeToken_ShouldContainOneTimeTokenInRequestUriAsync(string oneTimeToken)
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.OK, "xapp-get");

        // Act
        _ = await _subject.GetOneTimeTokenDataAsync(oneTimeToken);

        // Assert
        _httpMessageHandlerMock.AssertRequestUri(HttpMethod.Get, $"xapp/ott/{oneTimeToken}");
    }

    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    public void GetOneTimeTokenDataAsync_WithInvalidOneTimeToken_ShouldThrowExceptionAsync(string oneTimeToken)
    {
        // Act
        var ex = Assert.ThrowsAsync<ArgumentException>(() => _subject.GetOneTimeTokenDataAsync(oneTimeToken));

        // Assert
        Assert.IsNotNull(ex);
        Assert.That(ex!.Message, Is.EqualTo("Value cannot be null or white space (Parameter 'oneTimeToken')"));
    }

    [Test]
    [TestCase("abbeb856-311c-4742-b57f-c0b265404a8e", "1b9105dd-b7e7-456b-8303-3b9c7e48a622", "Device1", "7cdc3a7e20803a57c02711e25d1b92995abf9771")]
    [TestCase("fea70032-db11-4da3-a56b-91d2e409a807", "f0460610-0283-42fe-b246-fb0c20a3eda4", "Device2", "98fd8fcaff9fc32111378132c504881f04187b45")]
    public async Task ReFetchOneTimeTokenDataAsync_WithValidOneTimeTokenAndDeviceId_ShouldContainOneTimeTokenAndHashInRequestUriAsync(string apiSecret, string oneTimeToken, string deviceId, string expectedHash)
    {
        // Arrange
        var options = Options.Create(new ApiConfig
        {
            ApiKey = "00000000-0000-0000-0000-000000000000",
            ApiSecret = apiSecret
        });

        _xummHttpClient = new Mock<XummHttpClient>(
            _httpClientFactory.Object,
            options,
            new Mock<ILogger<XummHttpClient>>().Object);

        _subject = new XummXAppClient(_xummHttpClient.Object, options);

        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.OK, "xapp-get");

        // Act
        _ = await _subject.ReFetchOneTimeTokenDataAsync(oneTimeToken, deviceId);

        // Assert
        _httpMessageHandlerMock.AssertRequestUri(HttpMethod.Get, $"xapp/ott/{oneTimeToken}/{expectedHash}");
    }

    [Test]
    [TestCase("1b9105dd-b7e7-456b-8303-3b9c7e48a622", "Device1")]
    [TestCase("f0460610-0283-42fe-b246-fb0c20a3eda4", "Device2")]
    public async Task ReFetchOneTimeTokenDataAsync_WithValidOneTimeTokenAndDeviceId_ShouldReturnOneTimeTokenDataAsync(string oneTimeToken, string deviceId)
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.OK, "xapp-get");

        // Act
        var result = await _subject.ReFetchOneTimeTokenDataAsync(oneTimeToken, deviceId);

        // Assert
        AssertExtensions.AreEqual(XAppFixtures.XummXAppOtt, result);
    }

    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    public void ReFetchOneTimeTokenDataAsync_WithInvalidOneTimeToken_ShouldThrowExceptionAsync(string oneTimeToken)
    {
        // Act
        var ex = Assert.ThrowsAsync<ArgumentException>(() => _subject.ReFetchOneTimeTokenDataAsync(oneTimeToken, It.IsAny<string>()));

        // Assert
        Assert.IsNotNull(ex);
        Assert.That(ex!.Message, Is.EqualTo("Value cannot be null or white space (Parameter 'oneTimeToken')"));
    }

    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    public void ReFetchOneTimeTokenDataAsync_WithInvalidDeviceId_ShouldThrowExceptionAsync(string deviceId)
    {
        // Act
        var ex = Assert.ThrowsAsync<ArgumentException>(() => _subject.ReFetchOneTimeTokenDataAsync("1b9105dd-b7e7-456b-8303-3b9c7e48a622", deviceId));

        // Assert
        Assert.IsNotNull(ex);
        Assert.That(ex!.Message, Is.EqualTo("Value cannot be null or white space (Parameter 'deviceId')"));
    }
}
