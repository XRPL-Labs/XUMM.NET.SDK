using System;
using System.Linq;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using XUMM.Net.Clients;
using XUMM.Net.Configs;

namespace XUMM.Net.Tests.Clients;

[TestFixture]
public class XummHttpClientTests
{
    [SetUp]
    public void SetUp()
    {
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
    }

    private Mock<HttpMessageHandler> _httpMessageHandlerMock = default!;

    [Test]
    [TestCase("f6c4a1c7-d00b-4592-9eb9-f6e90ee836a0", "430ab92a-ccf8-4e8f-bd88-e65e1033acc3")]
    [TestCase("883b82d8-6b5b-4c34-a8e6-3d6019659182", "884b9650-5e52-420e-b0d9-b1206405bd46")]
    public void WhenValidCredentialsAreProvided_ShouldReturnHttpClient(string apiKey, string apiSecret)
    {
        //Arrange
        var xummHttpClient = new XummHttpClient(
            new HttpClient(_httpMessageHandlerMock.Object),
            Options.Create(new ApiConfig
            {
                ApiKey = apiKey,
                ApiSecret = apiSecret
            }),
            new Mock<ILogger<XummHttpClient>>().Object);

        //Act
        var httpClient = xummHttpClient.GetHttpClient(true);

        // Assert
        Assert.AreEqual(apiKey, httpClient.DefaultRequestHeaders.GetValues("X-API-Key").First());
        Assert.AreEqual(apiSecret, httpClient.DefaultRequestHeaders.GetValues("X-API-Secret").First());
    }

    [Test]
    [TestCase("xxxxxx", "430ab92a-ccf8-4e8f-bd88-e65e1033acc3", "Invalid API Key.")]
    [TestCase("f6c4a1c7-d00b-4592-9eb9-f6e90ee836a0", "yyyyyyy", "Invalid API Secret.")]
    public void WhenInvalidCredentialsAreProvided_ShouldThrowException(string apiKey, string apiSecret, string message)
    {
        // Assert
        var ex = Assert.Throws<Exception>(() =>
        {
            var client = new XummHttpClient(
                new HttpClient(_httpMessageHandlerMock.Object),
                Options.Create(new ApiConfig
                {
                    ApiKey = apiKey,
                    ApiSecret = apiSecret
                }),
                new Mock<ILogger<XummHttpClient>>().Object);
        });

        Assert.IsNotNull(ex);
        Assert.That(ex!.Message, Is.EqualTo(message));
    }
}
