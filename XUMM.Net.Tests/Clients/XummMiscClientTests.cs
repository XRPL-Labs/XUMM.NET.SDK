using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using XUMM.Net.Clients;
using XUMM.Net.Configs;
using XUMM.Net.Enums;
using XUMM.Net.Tests.Extensions;
using XUMM.Net.Tests.Fixtures;

namespace XUMM.Net.Tests.Clients;

[TestFixture]
public class XummMiscClientTests
{
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

        _xummMiscClient = new XummMiscClient(_xummHttpClient);
    }

    private XummHttpClient _xummHttpClient = default!;
    private XummMiscClient _xummMiscClient = default!;
    private Mock<HttpMessageHandler> _httpMessageHandlerMock = default!;
    private Mock<IHttpClientFactory> _httpClientFactory = default!;

    [Test]
    public async Task WhenPingIsRequested_ShouldReturnPongAsync()
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.OK, "pong");

        // Act
        var result = await _xummMiscClient.PingAsync();

        // Assert
        AssertExtensions.AreEqual(MiscFixtures.XummPong, result);
    }

    [Test]
    public async Task WhenCuratedAssetsAreRequested_ShouldReturnCuratedAssetsAsync()
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.OK, "curatedassets");

        // Act
        var result = await _xummMiscClient.GetCuratedAssetsAsync();

        // Assert
        AssertExtensions.AreEqual(MiscFixtures.XummCuratedAssets, result);
    }

    [Test]
    [TestCase("2557f69c-6617-40dc-9d1e-a34487cb3f90")]
    public async Task WhenKycStatusIsRequestedWithUserToken_ShouldReturnInProgressKycStatusAsync(string userToken)
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.OK, "kycstatus");

        // Act
        var result = await _xummMiscClient.GetKycStatusAsync(userToken);

        // Assert
        AssertExtensions.AreEqual(XummKycStatus.InProgress, result);
    }

    [Test]
    public async Task WhenTransactionIsRequested_ShouldReturnTransactionAsync()
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.OK, "xrpltx");

        // Act
        var result = await _xummMiscClient.GetTransactionAsync(It.IsAny<string>());

        // Assert
        AssertExtensions.AreEqual(MiscFixtures.XummTransaction, result);
    }
}
