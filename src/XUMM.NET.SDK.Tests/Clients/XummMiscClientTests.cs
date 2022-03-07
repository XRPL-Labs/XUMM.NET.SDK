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
using XUMM.NET.SDK.Enums;
using XUMM.NET.SDK.Tests.Extensions;
using XUMM.NET.SDK.Tests.Fixtures;

namespace XUMM.NET.SDK.Tests.Clients;

[TestFixture]
public class XummMiscClientTests
{
    private Mock<XummHttpClient> _xummHttpClient = default!;
    private Mock<HttpMessageHandler> _httpMessageHandlerMock = default!;
    private Mock<IHttpClientFactory> _httpClientFactory = default!;
    private XummMiscClient _subject = default!;

    [SetUp]
    public void SetUp()
    {
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>();

        _httpClientFactory = new Mock<IHttpClientFactory>();
        _httpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>()))
            .Returns(new HttpClient(_httpMessageHandlerMock.Object));

        _xummHttpClient = new Mock<XummHttpClient>(
            _httpClientFactory.Object,
            Options.Create(new ApiConfig
            {
                ApiKey = "00000000-0000-0000-0000-000000000000",
                ApiSecret = "00000000-0000-0000-0000-000000000000"
            }),
            new Mock<ILogger<XummHttpClient>>().Object);

        _subject = new XummMiscClient(_xummHttpClient.Object);
    }

    [Test]
    public async Task GetPingAsync_ShouldReturnPongAsync()
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.OK, "pong");

        // Act
        var result = await _subject.GetPingAsync();

        // Assert
        AssertExtensions.AreEqual(MiscFixtures.XummPong, result);
    }

    [Test]
    public async Task GetCuratedAssetsAsync_ShouldReturnCuratedAssetsAsync()
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.OK, "curated-assets");

        // Act
        var result = await _subject.GetCuratedAssetsAsync();

        // Assert
        AssertExtensions.AreEqual(MiscFixtures.XummCuratedAssets, result);
    }

    [Test]
    [TestCase("2557f69c-6617-40dc-9d1e-a34487cb3f90")]
    public async Task GetKycStatusAsync_WithUserToken_ShouldReturnKycStatusAsync(string userToken)
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.OK, "kycstatus-usertoken");

        // Act
        var result = await _subject.GetKycStatusAsync(userToken);

        // Assert
        AssertExtensions.AreEqual(XummKycStatus.InProgress, result);
    }

    [Test]
    [TestCase("2557f69c-6617-40dc-9d1e-a34487cb3f90")]
    public void GetKycStatusAsync_WithUserTokenAndInvalidStatus_ShouldThrowExceptionAsync(string userToken)
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.OK, "kycstatus-invalid");
        
        // Act
        var ex = Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _subject.GetKycStatusAsync(userToken));

        // Assert
        Assert.IsNotNull(ex);
        Assert.That(ex!.Message, Is.EqualTo($"Specified argument was out of the range of valid values. (Parameter 'name'){Environment.NewLine}Actual value was INVALID_STATUS."));
    }

    [Test]
    [TestCase("rDWLGshgAxSX2G4TEv3gA6QhtLgiXrWQXB")]
    public async Task GetKycStatusAsync_WithAccount_ShouldReturnKycStatusAsync(string account)
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.OK, "kycstatus-account");

        // Act
        var result = await _subject.GetKycStatusAsync(account);

        // Assert
        AssertExtensions.AreEqual(XummKycStatus.Successful, result);
    }

    [Test]
    [TestCase("2557f69c661740dc9d1ea34487cb3f90")]
    [TestCase("qrDWLGshgAxSX2G4TEv3gA6QhtLgiXrWQXB")]
    public void GetKycStatusAsync_WithInvalidUserTokenAndAccount_ShouldThrowExceptionAsync(string userTokenOrAccount)
    {
        // Act
        var ex = Assert.ThrowsAsync<ArgumentException>(() => _subject.GetKycStatusAsync(userTokenOrAccount));

        // Assert
        Assert.IsNotNull(ex);
        Assert.That(ex!.Message, Is.EqualTo("Invalid user token or account provided (Parameter 'userTokenOrAccount')"));
    }

    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    public void GetKycStatusAsync_WithNullOrWhiteSpaceUserTokenAndAccount_ShouldThrowExceptionAsync(string userTokenOrAccount)
    {
        // Act
        var ex = Assert.ThrowsAsync<ArgumentException>(() => _subject.GetKycStatusAsync(userTokenOrAccount));

        // Assert
        Assert.IsNotNull(ex);
        Assert.That(ex!.Message, Is.EqualTo("Value cannot be null or white space (Parameter 'userTokenOrAccount')"));
    }

    [Test]
    [TestCase("INR")]
    public async Task GetRatesAsync_WithValidCurrencyCode_ShouldReturnRatesAsync(string currencyCode)
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.OK, "rates");

        // Act
        var result = await _subject.GetRatesAsync(currencyCode);

        // Assert
        AssertExtensions.AreEqual(MiscFixtures.XummRates, result);
    }

    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    public void GetRatesAsync_WithInvalidCurrencyCode_ShouldThrowExceptionAsync(string currencyCode)
    {
        // Act
        var ex = Assert.ThrowsAsync<ArgumentException>(() => _subject.GetRatesAsync(currencyCode));

        // Assert
        Assert.IsNotNull(ex);
        Assert.That(ex!.Message, Is.EqualTo("Value cannot be null or white space (Parameter 'currencyCode')"));
    }

    [Test]
    [TestCase("C3951A3229506DB2C505ED248EFD3BBD8F232C7684732F38270BE9DE90F75134")]
    public async Task GetTransactionAsync_WithValidTxHash_ShouldReturnTransactionAsync(string txHash)
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.OK, "xrpltx");

        // Act
        var result = await _subject.GetTransactionAsync(txHash);

        // Assert
        AssertExtensions.AreEqual(MiscFixtures.XummTransaction, result);
    }

    [Test]
    [TestCase("C3951A3229506DB2C505ED248EFD3BBD8F232C7684732F38270BE9DE90F75134")]
    public async Task GetTransactionAsync_WithValidTxHash_ShouldContainTxHashInRequestUriAsync(string txHash)
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.OK, "xrpltx");

        // Act
        var result = await _subject.GetTransactionAsync(txHash);

        // Assert
        _httpMessageHandlerMock.AssertRequestUri(HttpMethod.Get, $"/xrpl-tx/{txHash}");
    }

    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    public void GetTransactionAsync_WithInvalidTxHash_ShouldThrowExceptionAsync(string txHash)
    {
        // Act
        var ex = Assert.ThrowsAsync<ArgumentException>(() => _subject.GetTransactionAsync(txHash));

        // Assert
        Assert.IsNotNull(ex);
        Assert.That(ex!.Message, Is.EqualTo("Value cannot be null or white space (Parameter 'txHash')"));
    }

    [Test]
    [TestCase(null, 50, 5)]
    [TestCase("", 100, 0)]
    [TestCase(" ", 199, 2)]
    public void GetAvatarUrl_WithInvalidAccount_ShouldThrowExceptionAsync(string account, int dimensions, int padding)
    {
        // Act
        var ex = Assert.Throws<ArgumentException>(() => _subject.GetAvatarUrl(account, dimensions, padding));

        // Assert
        Assert.IsNotNull(ex);
        Assert.That(ex!.Message, Is.EqualTo("Value cannot be null or white space (Parameter 'account')"));
    }

    [Test]
    [TestCase("rDWLGshgAxSX2G4TEv3gA6QhtLgiXrWQXB", 50, 5)]
    [TestCase("rDWLGshgAxSX2G4TEv3gA6QhtLgiXrWQXB", 100, 0)]
    [TestCase("rDWLGshgAxSX2G4TEv3gA6QhtLgiXrWQXB", 199, 2)]

    public void GetAvatarUrl_WithInvalidDimensions_ShouldThrowException(string account, int dimensions, int padding)
    {
        // Act
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => _subject.GetAvatarUrl(account, dimensions, padding));

        // Assert
        Assert.IsNotNull(ex);
        Assert.That(ex!.Message, Is.EqualTo("The minimum (square) dimensions are 200. (Parameter 'dimensions')"));
    }

    [Test]

    [TestCase("rDWLGshgAxSX2G4TEv3gA6QhtLgiXrWQXB", 200, -50)]
    [TestCase("rDWLGshgAxSX2G4TEv3gA6QhtLgiXrWQXB", 250, -1)]
    public void GetAvatarUrl_WithInvalidPadding_ShouldThrowException(string account, int dimensions, int padding)
    {
        // Act
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => _subject.GetAvatarUrl(account, dimensions, padding));

        // Asert
        Assert.IsNotNull(ex);
        Assert.That(ex!.Message, Is.EqualTo("The padding should be equal or greater than zero. (Parameter 'padding')"));
    }

    [Test]
    [TestCase("rDWLGshgAxSX2G4TEv3gA6QhtLgiXrWQXB", 200, 5, "https://xumm.app/avatar/rDWLGshgAxSX2G4TEv3gA6QhtLgiXrWQXB_200_5.png")]
    [TestCase("rDWLGshgAxSX2G4TEv3gA6QhtLgiXrWQXB", 250, 0, "https://xumm.app/avatar/rDWLGshgAxSX2G4TEv3gA6QhtLgiXrWQXB_250_0.png")]
    [TestCase("rDWLGshgAxSX2G4TEv3gA6QhtLgiXrWQXB", 500, 2, "https://xumm.app/avatar/rDWLGshgAxSX2G4TEv3gA6QhtLgiXrWQXB_500_2.png")]
    public void GetAvatarUrl_WithValidDimensions_ShouldReturnAvatarUrl(string account, int dimensions, int padding, string expected)
    {
        // Act
        var result = _subject.GetAvatarUrl(account, dimensions, padding);

        // Assert
        AssertExtensions.AreEqual(expected, result);
    }
}
