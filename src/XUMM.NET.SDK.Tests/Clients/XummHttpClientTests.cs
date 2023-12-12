using System;
using System.Linq;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using XUMM.NET.SDK.Clients;
using XUMM.NET.SDK.Configs;

namespace XUMM.NET.SDK.Tests.Clients;

[TestFixture]
public class XummHttpClientTests
{
    private Mock<HttpMessageHandler> _httpMessageHandlerMock = default!;
    private Mock<IHttpClientFactory> _httpClientFactory = default!;

    [SetUp]
    public void SetUp()
    {
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
        _httpClientFactory = new Mock<IHttpClientFactory>();
        _httpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>()))
            .Returns(new HttpClient(_httpMessageHandlerMock.Object));
    }

    [Test]
    [TestCase("f6c4a1c7-d00b-4592-9eb9-f6e90ee836a0", "430ab92a-ccf8-4e8f-bd88-e65e1033acc3")]
    [TestCase("883b82d8-6b5b-4c34-a8e6-3d6019659182", "884b9650-5e52-420e-b0d9-b1206405bd46")]
    public void GetHttpClient_WithValidCredentials_ShouldReturnHttpClient(string apiKey, string apiSecret)
    {
        // Arrange
        var xummHttpClient = new XummHttpClient(
            _httpClientFactory.Object,
            Options.Create(new ApiConfig
            {
                ApiKey = apiKey,
                ApiSecret = apiSecret
            }),
            new Mock<ILogger<XummHttpClient>>().Object);

        // Act
        var httpClient = xummHttpClient.GetHttpClient(true);

        // Assert
        Assert.That(apiKey, Is.EqualTo(httpClient.DefaultRequestHeaders.GetValues("X-API-Key").First()));
        Assert.That(apiSecret, Is.EqualTo(httpClient.DefaultRequestHeaders.GetValues("X-API-Secret").First()));
    }

    [Test]
    [TestCase("xxxxxx", "430ab92a-ccf8-4e8f-bd88-e65e1033acc3", "Invalid API Key.")]
    [TestCase("f6c4a1c7-d00b-4592-9eb9-f6e90ee836a0", "yyyyyyy", "Invalid API Secret.")]
    public void XummHttpClient_WithInvalidCredentials_ShouldThrowException(string apiKey, string apiSecret, string message)
    {
        // Arrange & Act
        var ex = Assert.Throws<Exception>(() => new XummHttpClient(
                _httpClientFactory.Object,
                Options.Create(new ApiConfig
                {
                    ApiKey = apiKey,
                    ApiSecret = apiSecret
                }),
                new Mock<ILogger<XummHttpClient>>().Object));

        // Assert
        Assert.That(ex, Is.Not.Null);
        Assert.That(ex!.Message, Is.EqualTo(message));
    }
}
