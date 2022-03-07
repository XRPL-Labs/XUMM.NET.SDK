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
public class XummMiscAppStorageClientTests
{
    private Mock<XummHttpClient> _xummHttpClient = default!;
    private Mock<HttpMessageHandler> _httpMessageHandlerMock = default!;
    private Mock<IHttpClientFactory> _httpClientFactory = default!;
    private XummMiscAppStorageClient _subject = default!;

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

        _subject = new XummMiscAppStorageClient(_xummHttpClient.Object);
    }

    [Test]
    public async Task GetAsync_ShouldReturnXummStorageAsync()
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.OK, "storage-get");

        // Act
        var result = await _subject.GetAsync();

        // Assert
        AssertExtensions.AreEqual(MiscAppStorageFixtures.XummStorage, result);
    }

    [Test]
    public async Task StoreAsync_WithAnyValue_ShouldReturnXummStorageStoreAsync()
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.OK, "storage-set");

        // Act
        var result = await _subject.StoreAsync(It.IsAny<string>());

        // Assert
        AssertExtensions.AreEqual(MiscAppStorageFixtures.XummStorageStore, result);
    }

    [Test]
    public async Task ClearAsync_ShouldReturnEmptyDataAsync()
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.OK, "storage-delete");

        // Act
        var result = await _subject.ClearAsync();

        // Assert
        AssertExtensions.AreEqual(MiscAppStorageFixtures.XummStorageDelete, result);
    }

    [Test]
    public void GetAsync_WithInvalidCredentials_ShouldThrowException()
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.Forbidden, "invalid-credentials");

        // Act
        var ex = Assert.ThrowsAsync<HttpRequestException>(async () => await _subject.GetAsync());

        // Assert
        Assert.IsNotNull(ex);
        Assert.That(ex!.Message, Is.EqualTo("Error code 813, see XUMM Dev Console, reference: '26279bfe-c7e1-4b12-a680-26119d8f5062'."));
    }

    [Test]
    public void StoreAsync_WithInvalidCredentials_ShouldThrowException()
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.Forbidden, "invalid-credentials");

        // Act
        var ex = Assert.ThrowsAsync<HttpRequestException>(async () => await _subject.StoreAsync(It.IsAny<string>()));

        // Assert
        Assert.IsNotNull(ex);
        Assert.That(ex!.Message, Is.EqualTo("Error code 813, see XUMM Dev Console, reference: '26279bfe-c7e1-4b12-a680-26119d8f5062'."));
    }

    [Test]
    public void ClearAsync_WithInvalidCredentials_ShouldThrowException()
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.Forbidden, "invalid-credentials");

        // Act
        var ex = Assert.ThrowsAsync<HttpRequestException>(async () => await _subject.ClearAsync());

        // Asert
        Assert.IsNotNull(ex);
        Assert.That(ex!.Message, Is.EqualTo("Error code 813, see XUMM Dev Console, reference: '26279bfe-c7e1-4b12-a680-26119d8f5062'."));
    }
}
