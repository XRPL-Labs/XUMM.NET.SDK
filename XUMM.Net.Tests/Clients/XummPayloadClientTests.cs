using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using XUMM.Net.Clients;
using XUMM.Net.Clients.Interfaces;
using XUMM.Net.Configs;
using XUMM.Net.Enums;
using XUMM.Net.Tests.Extensions;
using XUMM.Net.Tests.Fixtures;

namespace XUMM.Net.Tests.Clients;

[TestFixture]
public class XummPayloadClientTests
{
    private XummHttpClient _xummHttpClient = default!;
    private XummPayloadClient _xummPayloadClient = default!;
    private Mock<HttpMessageHandler> _httpMessageHandlerMock = default!;
    private Mock<IHttpClientFactory> _httpClientFactory = default!;

    [SetUp]
    public void SetUp()
    {
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>();

        _httpClientFactory = new Mock<IHttpClientFactory>();
        _httpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>()))
            .Returns(new HttpClient(_httpMessageHandlerMock.Object));

        _xummHttpClient = new XummHttpClient(
            _httpClientFactory.Object,
            Options.Create(new ApiConfig
            {
                ApiKey = "00000000-0000-0000-0000-000000000000",
                ApiSecret = "00000000-0000-0000-0000-000000000000"
            }),
            new Mock<ILogger<XummHttpClient>>().Object);

        _xummPayloadClient = new XummPayloadClient(_xummHttpClient, 
            new Mock<ILogger<IXummPayloadClient>>().Object);
    }

    [Test]
    public async Task WhenCancelIsRequested_ShouldCancelPayloadAsync()
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.OK, "payload-cancel");

        // Act
        var result = await _xummPayloadClient.CancelAsync(It.IsAny<string>());

        // Assert
        AssertExtensions.AreEqual(PayloadFixtures.XummDeletePayload, result!);
    }

    [Test]
    public async Task WhenCancelIsRequestedWithUnknownUuid_ShouldReturnNullAsync()
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.NotFound, "payload-notfound");

        // Act
        var result = await _xummPayloadClient.CancelAsync(It.IsAny<string>());

        // Assert
        Assert.IsNull(result);
    }
    
    [Test]
    public void WhenCancelIsRequestedWithUnknownUuid_ShouldThrowExceptionAsync()
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.NotFound, "payload-notfound");

        // Act
        var ex = Assert.ThrowsAsync<HttpRequestException>(async () => await _xummPayloadClient.CancelAsync(It.IsAny<string>(), true));

        // Assert
        Assert.IsNotNull(ex);
        Assert.That(ex!.Message, Is.EqualTo("Error code 404, see XUMM Dev Console, reference: 'a61ba59a-0304-44ae-a86e-d74808bd5190'."));
    }
}
