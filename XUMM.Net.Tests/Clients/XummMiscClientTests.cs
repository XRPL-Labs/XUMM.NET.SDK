using System;
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
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.OK, "kycstatus-usertoken");

        // Act
        var result = await _xummMiscClient.GetKycStatusAsync(userToken);

        // Assert
        AssertExtensions.AreEqual(XummKycStatus.InProgress, result);
    }

    [Test]
    [TestCase("rDWLGshgAxSX2G4TEv3gA6QhtLgiXrWQXB")]
    public async Task WhenKycStatusIsRequestedWithAccount_ShouldReturnInProgressKycStatusAsync(string account)
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.OK, "kycstatus-account");

        // Act
        var result = await _xummMiscClient.GetKycStatusAsync(account);

        // Assert
        AssertExtensions.AreEqual(XummKycStatus.Successful, result);
    }

    [Test]
    [TestCase("2557f69c661740dc9d1ea34487cb3f90")]
    [TestCase("qrDWLGshgAxSX2G4TEv3gA6QhtLgiXrWQXB")]
    public async Task WhenKycStatusIsRequestedWithInvalidUserTokenAndAccount_ShouldReturnNoneKycStatusAsync(string userTokenOrAccount)
    {
        // Act
        var result = await _xummMiscClient.GetKycStatusAsync(userTokenOrAccount);

        // Assert
        AssertExtensions.AreEqual(XummKycStatus.None, result);
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

    [Test]
    [TestCase(50)]
    [TestCase(100)]
    [TestCase(199)]
    public void WhenInvalidAvatarDimensionsAreProvided_ShouldThrowException(int dimensions)
    {
        // Assert
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            _xummMiscClient.GetAvatarUrl(It.IsAny<string>(), dimensions, It.IsAny<int>());
        });

        Assert.IsNotNull(ex);
        Assert.That(ex!.Message, Is.EqualTo("The minimum (square) dimensions are 200. (Parameter 'dimensions')"));
    }

    [Test]
    [TestCase(-50)]
    [TestCase(-1)]
    public void WhenInvalidAvatarPaddingIsProvided_ShouldThrowException(int padding)
    {
        // Assert
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            _xummMiscClient.GetAvatarUrl(It.IsAny<string>(), 200, padding);
        });

        Assert.IsNotNull(ex);
        Assert.That(ex!.Message, Is.EqualTo("The padding should be equal or greater than zero. (Parameter 'padding')"));
    }

    [Test]
    [TestCase("rDWLGshgAxSX2G4TEv3gA6QhtLgiXrWQXB", 200, 5, "https://xumm.app/avatar/rDWLGshgAxSX2G4TEv3gA6QhtLgiXrWQXB_200_5.png")]
    [TestCase("rDWLGshgAxSX2G4TEv3gA6QhtLgiXrWQXB", 250, 0, "https://xumm.app/avatar/rDWLGshgAxSX2G4TEv3gA6QhtLgiXrWQXB_250_0.png")]
    [TestCase("rDWLGshgAxSX2G4TEv3gA6QhtLgiXrWQXB", 500, 2, "https://xumm.app/avatar/rDWLGshgAxSX2G4TEv3gA6QhtLgiXrWQXB_500_2.png")]
    public void WhenValidAvatarDetailsAreProvided_ShouldReturnTheAvatarUrl(string account, int dimensions, int padding, string expected)
    {
        // Act
        var result = _xummMiscClient.GetAvatarUrl(account, dimensions, padding);

        // Assert
        AssertExtensions.AreEqual(expected, result);
    }
}
