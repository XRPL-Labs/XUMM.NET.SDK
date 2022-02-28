using System.Collections.Generic;
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
using XUMM.Net.Models.Payload;
using XUMM.Net.Models.Payload.Xumm;
using XUMM.Net.Tests.Extensions;
using XUMM.Net.Tests.Fixtures;
using XUMM.Net.WebSocket;
using XUMM.Net.WebSocket.EventArgs;

namespace XUMM.Net.Tests.Clients;

[TestFixture]
public class XummPayloadClientTests
{
    private XummHttpClient _xummHttpClient = default!;
    private XummPayloadClient _xummPayloadClient = default!;
    private Mock<HttpMessageHandler> _httpMessageHandlerMock = default!;
    private Mock<IHttpClientFactory> _httpClientFactory = default!;
    private Mock<IXummWebSocket> _xummWebSocket = default!;

    [SetUp]
    public void SetUp()
    {
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>();

        _httpClientFactory = new Mock<IHttpClientFactory>();
        _httpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>()))
            .Returns(new HttpClient(_httpMessageHandlerMock.Object));

        _xummWebSocket = new Mock<IXummWebSocket>();

        _xummHttpClient = new XummHttpClient(
            _httpClientFactory.Object,
            Options.Create(new ApiConfig
            {
                ApiKey = "00000000-0000-0000-0000-000000000000",
                ApiSecret = "00000000-0000-0000-0000-000000000000"
            }),
            new Mock<ILogger<XummHttpClient>>().Object);

        _xummPayloadClient = new XummPayloadClient(
            _xummHttpClient,
            _xummWebSocket.Object);
    }

    #region CreateAsync Tests
    [Test]
    public async Task CreateAsync_WithValidXummPostJsonPayload_ShouldReturnXummPayloadResponseAsync()
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.OK, "payload-create");

        // Act
        var result = await _xummPayloadClient.CreateAsync(It.IsAny<XummPostJsonPayload>());

        // Assert
        AssertExtensions.AreEqual(PayloadFixtures.XummCreatePayload, result!);
    }

    [Test]
    public async Task CreateAsync_WithValidXummPayloadTransaction_ShouldReturnCreatedPayloadAsync()
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.OK, "payload-create");

        var transaction = new XummPayloadTransaction(XummTransactionType.SignIn)
        {
            { "Destination", "rPEPPER7kfTD9w2To4CQk6UCfuHM9c6GDY" },
            { "DestinationTag", "495" },
        };

        // Act
        var result = await _xummPayloadClient.CreateAsync(transaction);

        // Assert
        AssertExtensions.AreEqual(PayloadFixtures.XummCreatePayload, result!);
    }

    [Test]
    public async Task CreateAsync_WithInvalidXummPostJsonPayload_ShouldReturnNullAsync()
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.InternalServerError, "payload-error");

        // Act
        var result = await _xummPayloadClient.CreateAsync(It.IsAny<XummPostJsonPayload>(), false);

        // Assert
        Assert.IsNull(result);
    }

    [Test]
    public void CreateAsync_WithInvalidXummPostJsonPayload_ShouldThrowExceptionAsync()
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.InternalServerError, "payload-error");

        // Act
        var ex = Assert.ThrowsAsync<HttpRequestException>(async () => await _xummPayloadClient.CreateAsync(It.IsAny<XummPostJsonPayload>(), true));

        // Assert
        Assert.IsNotNull(ex);
        Assert.That(ex!.Message, Is.EqualTo("Error code 602, see XUMM Dev Console, reference: 'a61ba59a-0304-44ae-a86e-d74808bd5190'."));
    }
    #endregion

    #region GetAsync Tests
    [Test]
    [TestCase("00000000-0000-4839-af2f-f794874a80b0")]
    public async Task GetAsync_WithValidPayloadUuid_ShouldReturnPayloadAsync(string payloadUuid)
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.OK, "payload-get");

        // Act
        var result = await _xummPayloadClient.GetAsync(payloadUuid);

        // Assert
        AssertExtensions.AreEqual(PayloadFixtures.XummPayloadDetails, result!);
    }

    [Test]
    public async Task GetAsync_WithInvalidPayloadUuid_ShouldReturnNullAsync()
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.InternalServerError, "payload-error");

        // Act
        var result = await _xummPayloadClient.GetAsync(It.IsAny<string>(), false);

        // Assert
        Assert.IsNull(result);
    }

    [Test]
    public void GetAsync_WithInvalidPayloadUuid_ShouldThrowExceptionAsync()
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.InternalServerError, "payload-error");

        // Act
        var ex = Assert.ThrowsAsync<HttpRequestException>(async () => await _xummPayloadClient.GetAsync(It.IsAny<string>(), true));

        // Assert
        Assert.IsNotNull(ex);
        Assert.That(ex!.Message, Is.EqualTo("Error code 602, see XUMM Dev Console, reference: 'a61ba59a-0304-44ae-a86e-d74808bd5190'."));
    }
    #endregion

    #region CancelAsync Tests
    [Test]
    [TestCase("00000000-0000-4839-af2f-f794874a80b0")]
    public async Task CancelAsync_WithValidPayloadUuid_ShouldCancelPayloadAsync(string payloadUuid)
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.OK, "payload-cancel");

        // Act
        var result = await _xummPayloadClient.CancelAsync(payloadUuid);

        // Assert
        AssertExtensions.AreEqual(PayloadFixtures.XummDeletePayload, result!);
    }

    [Test]
    [TestCase("00000000-0000-4839-af2f-f794874a80b0")]
    public async Task CancelAsync_WithValidPayloadUuid_ShouldContainPayloadUuidInRequestUriAsync(string payloadUuid)
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.OK, "payload-cancel");

        // Act
        _ = await _xummPayloadClient.CancelAsync(payloadUuid);

        // Assert
        _httpMessageHandlerMock.AssertRequestUri(HttpMethod.Delete, $"/payload/{payloadUuid}");
    }

    [Test]
    [TestCase("00000000-0000-4839-af2f-f794874a80b0")]
    public async Task CancelAsync_WithCreatedPayload_ShouldCancelPayloadAsync(string payloadUuid)
    {
        // Arrange 
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.OK, "payload-cancel");

        var payloadDetails = new XummPayloadResponse
        {
            Uuid = payloadUuid
        };

        // Act
        var result = await _xummPayloadClient.CancelAsync(payloadDetails!);

        // Assert
        AssertExtensions.AreEqual(PayloadFixtures.XummDeletePayload, result!);
    }

    [Test]
    [TestCase("00000000-0000-4839-af2f-f794874a80b0")]
    public async Task CancelAsync_WithCreatedPayload_ShouldContainPayloadUuidInRequestUriAsync(string payloadUuid)
    {
        // Arrange 
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.OK, "payload-cancel");

        var payloadDetails = new XummPayloadResponse
        {
            Uuid = payloadUuid
        };

        // Act
        _ = await _xummPayloadClient.CancelAsync(payloadDetails!);

        // Assert
        _httpMessageHandlerMock.AssertRequestUri(HttpMethod.Delete, $"/payload/{payloadUuid}");
    }

    [Test]
    [TestCase("00000000-0000-4839-af2f-f794874a80b0")]
    public async Task CancelAsync_WithFetchedPayload_ShouldCancelPayloadAsync(string payloadUuid)
    {
        // Arrange 
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.OK, "payload-cancel");

        var payloadDetails = new XummPayloadDetails
        {
            Meta = new XummPayloadDetailsMeta
            {
                Uuid = payloadUuid
            }
        };

        // Act
        var result = await _xummPayloadClient.CancelAsync(payloadDetails!);

        // Assert
        AssertExtensions.AreEqual(PayloadFixtures.XummDeletePayload, result!);
    }

    [Test]
    [TestCase("00000000-0000-4839-af2f-f794874a80b0")]
    public async Task CancelAsync_WithFetchedPayload_ShouldContainPayloadUuidInRequestUriAsync(string payloadUuid)
    {
        // Arrange 
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.OK, "payload-cancel");

        var payloadDetails = new XummPayloadDetails
        {
            Meta = new XummPayloadDetailsMeta
            {
                Uuid = payloadUuid
            }
        };

        // Act
        _ = await _xummPayloadClient.CancelAsync(payloadDetails!);

        // Assert
        _httpMessageHandlerMock.AssertRequestUri(HttpMethod.Delete, $"/payload/{payloadUuid}");
    }

    [Test]
    public async Task CancelAsync_WithInvalidPayloadUuid_ShouldReturnNullAsync()
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.NotFound, "payload-notfound");

        // Act
        var result = await _xummPayloadClient.CancelAsync(It.IsAny<string>());

        // Assert
        Assert.IsNull(result);
    }

    [Test]
    public void CancelAsync_WithInvalidPayloadUuid_ShouldThrowExceptionAsync()
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.NotFound, "payload-notfound");

        // Act
        var ex = Assert.ThrowsAsync<HttpRequestException>(async () => await _xummPayloadClient.CancelAsync(It.IsAny<string>(), true));

        // Assert
        Assert.IsNotNull(ex);
        Assert.That(ex!.Message, Is.EqualTo("Error code 404, see XUMM Dev Console, reference: 'a61ba59a-0304-44ae-a86e-d74808bd5190'."));
    }
    #endregion
}
