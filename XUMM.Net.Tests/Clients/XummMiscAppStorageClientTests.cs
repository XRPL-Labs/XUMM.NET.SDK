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
using XUMM.Net.Tests.Extensions;
using XUMM.Net.Tests.Fixtures;

namespace XUMM.Net.Tests.Clients;

[TestFixture]
public class XummMiscAppStorageClientTests
{
    private XummHttpClient _xummHttpClient = default!;
    private XummMiscAppStorageClient _xummMiscAppStorageClient = default!;
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

        _xummMiscAppStorageClient = new XummMiscAppStorageClient(_xummHttpClient);
    }

    [Test]
    public async Task WhenAppStorageIsRequested_ShouldReturnAppStorageAsync()
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.OK, "storage-get");

        // Act
        var result = await _xummMiscAppStorageClient.GetAsync();

        // Assert
        AssertExtensions.AreEqual(MiscAppStorageFixtures.XummStorage, result);
    }

    [Test]
    public async Task WhenAppStorageIsStored_ShouldReturnAppStorageStoreAsync()
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.OK, "storage-set");

        // Act
        var result = await _xummMiscAppStorageClient.StoreAsync(It.IsAny<string>());

        // Assert
        AssertExtensions.AreEqual(MiscAppStorageFixtures.XummStorageStore, result);
    }

    [Test]
    public async Task WhenAppStorageIsCleared_ShouldReturnAppStorageStoreWithoutDataAsync()
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.OK, "storage-delete");

        // Act
        var result = await _xummMiscAppStorageClient.ClearAsync();

        // Assert
        AssertExtensions.AreEqual(MiscAppStorageFixtures.XummStorageDelete, result);
    }
    
    [Test]
    public void WhenAppStorageIsRequestedAndInvalidCredentialsAreProvided_ShouldThrowException()
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.Forbidden, "invalid-credentials");

        // Act & Assert
        var ex = Assert.ThrowsAsync<HttpRequestException>(async () =>
        {
            var result = await _xummMiscAppStorageClient.GetAsync();
        });

        Assert.IsNotNull(ex);
        Assert.That(ex!.Message, Is.EqualTo("Error code 813, see XUMM Dev Console, reference: '26279bfe-c7e1-4b12-a680-26119d8f5062'."));
    }

    [Test]
    public void WhenAppStorageIsStoredAndInvalidCredentialsAreProvided_ShouldReturnAppStorageStoreAsync()
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.Forbidden, "invalid-credentials");

        // Act & Assert
        var ex = Assert.ThrowsAsync<HttpRequestException>(async () =>
        {
            var result = await _xummMiscAppStorageClient.StoreAsync(It.IsAny<string>());
        });

        Assert.IsNotNull(ex);
        Assert.That(ex!.Message, Is.EqualTo("Error code 813, see XUMM Dev Console, reference: '26279bfe-c7e1-4b12-a680-26119d8f5062'."));
    }

    [Test]
    public void WhenAppStorageIsClearedAndInvalidCredentialsAreProvided_ShouldReturnAppStorageStoreWithoutDataAsync()
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.Forbidden, "invalid-credentials");

        // Act & Assert
        var ex = Assert.ThrowsAsync<HttpRequestException>(async () =>
        {
            var result = await _xummMiscAppStorageClient.ClearAsync();
        });

        Assert.IsNotNull(ex);
        Assert.That(ex!.Message, Is.EqualTo("Error code 813, see XUMM Dev Console, reference: '26279bfe-c7e1-4b12-a680-26119d8f5062'."));
    }
}
