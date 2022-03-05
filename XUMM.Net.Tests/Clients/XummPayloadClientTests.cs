using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using XUMM.Net.Clients;
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
    private Mock<XummHttpClient> _xummHttpClient = default!;
    private Mock<HttpMessageHandler> _httpMessageHandlerMock = default!;
    private Mock<IHttpClientFactory> _httpClientFactory = default!;
    private Mock<IXummWebSocket> _xummWebSocket = default!;
    private XummPayloadClient _subject = default!;

    [SetUp]
    public void SetUp()
    {
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>();

        _httpClientFactory = new Mock<IHttpClientFactory>();
        _httpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>()))
            .Returns(new HttpClient(_httpMessageHandlerMock.Object));

        _xummWebSocket = new Mock<IXummWebSocket>();
        _xummWebSocket.Setup(x => x.SubscribeAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(GetWebSocketMessagesAsync);

        _xummHttpClient = new Mock<XummHttpClient>(
            _httpClientFactory.Object,
            Options.Create(new ApiConfig
            {
                ApiKey = "00000000-0000-0000-0000-000000000000",
                ApiSecret = "00000000-0000-0000-0000-000000000000"
            }),
            new Mock<ILogger<XummHttpClient>>().Object);

        _subject = new XummPayloadClient(
            _xummHttpClient.Object,
            _xummWebSocket.Object);
    }

    #region CreateAsync Tests
    [Test]
    public async Task CreateAsync_WithValidXummPostJsonPayload_ShouldReturnXummPayloadResponseAsync()
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.OK, "payload-create");

        // Act
        var result = await _subject.CreateAsync(It.IsAny<XummPostJsonPayload>());

        // Assert
        AssertExtensions.AreEqual(PayloadFixtures.XummCreatePayload, result!);
    }

    [Test]
    public async Task CreateAsync_WithInvalidXummPostJsonPayload_ShouldReturnNullAsync()
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.InternalServerError, "payload-error");

        // Act
        var result = await _subject.CreateAsync(It.IsAny<XummPostJsonPayload>(), false);

        // Assert
        Assert.IsNull(result);
    }

    [Test]
    public void CreateAsync_WithInvalidXummPostJsonPayload_ShouldThrowExceptionAsync()
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.InternalServerError, "payload-error");

        // Act
        var ex = Assert.ThrowsAsync<HttpRequestException>(async () => await _subject.CreateAsync(It.IsAny<XummPostJsonPayload>(), true));

        // Assert
        Assert.IsNotNull(ex);
        Assert.That(ex!.Message, Is.EqualTo("Error code 602, see XUMM Dev Console, reference: 'a61ba59a-0304-44ae-a86e-d74808bd5190'."));
    }

    [Test]
    public async Task CreateAsync_WithValidXummPostBlobPayload_ShouldReturnXummPayloadResponseAsync()
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.OK, "payload-create");

        // Act
        var result = await _subject.CreateAsync(It.IsAny<XummPostBlobPayload>());

        // Assert
        AssertExtensions.AreEqual(PayloadFixtures.XummCreatePayload, result!);
    }

    [Test]
    public async Task CreateAsync_WithInvalidXummPostBlobPayload_ShouldReturnNullAsync()
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.InternalServerError, "payload-error");

        // Act
        var result = await _subject.CreateAsync(It.IsAny<XummPostBlobPayload>(), false);

        // Assert
        Assert.IsNull(result);
    }

    [Test]
    public void CreateAsync_WithInvalidXummPostBlobPayload_ShouldThrowExceptionAsync()
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.InternalServerError, "payload-error");

        // Act
        var ex = Assert.ThrowsAsync<HttpRequestException>(async () => await _subject.CreateAsync(It.IsAny<XummPostBlobPayload>(), true));

        // Assert
        Assert.IsNotNull(ex);
        Assert.That(ex!.Message, Is.EqualTo("Error code 602, see XUMM Dev Console, reference: 'a61ba59a-0304-44ae-a86e-d74808bd5190'."));
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
        var result = await _subject.CreateAsync(transaction);

        // Assert
        AssertExtensions.AreEqual(PayloadFixtures.XummCreatePayload, result!);
    }

    [Test]
    public async Task CreateAsync_WithInvalidXummPayloadTransaction_ShouldReturnNullAsync()
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.InternalServerError, "payload-error");

        // Act
        var result = await _subject.CreateAsync(It.IsAny<XummPayloadTransaction>(), false);

        // Assert
        Assert.IsNull(result);
    }

    [Test]
    public void CreateAsync_WithInvalidXummPayloadTransaction_ShouldThrowExceptionAsync()
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.InternalServerError, "payload-error");

        // Act
        var ex = Assert.ThrowsAsync<HttpRequestException>(async () => await _subject.CreateAsync(It.IsAny<XummPayloadTransaction>(), true));

        // Assert
        Assert.IsNotNull(ex);
        Assert.That(ex!.Message, Is.EqualTo("Error code 602, see XUMM Dev Console, reference: 'a61ba59a-0304-44ae-a86e-d74808bd5190'."));
    }

    [Test]
    public void CreateAsync_WhenInternalServerErrorOccurs_ShouldThrowExceptionAsync()
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.InternalServerError, "payload-fatal");

        // Act
        var ex = Assert.ThrowsAsync<HttpRequestException>(async () => await _subject.CreateAsync(It.IsAny<XummPayloadTransaction>(), true));

        // Assert
        Assert.IsNotNull(ex);
        Assert.That(ex!.Message, Is.EqualTo("Some error has occured"));
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
        var result = await _subject.GetAsync(payloadUuid);

        // Assert
        AssertExtensions.AreEqual(PayloadFixtures.XummPayloadDetails, result!);
    }

    [Test]
    public async Task GetAsync_WithInvalidPayloadUuid_ShouldReturnNullAsync()
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.InternalServerError, "payload-error");

        // Act
        var result = await _subject.GetAsync(It.IsAny<string>(), false);

        // Assert
        Assert.IsNull(result);
    }

    [Test]
    public void GetAsync_WithInvalidPayloadUuid_ShouldThrowExceptionAsync()
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.InternalServerError, "payload-error");

        // Act
        var ex = Assert.ThrowsAsync<HttpRequestException>(async () => await _subject.GetAsync(It.IsAny<string>(), true));

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
        var result = await _subject.CancelAsync(payloadUuid);

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
        _ = await _subject.CancelAsync(payloadUuid);

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
        var result = await _subject.CancelAsync(payloadDetails!);

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
        _ = await _subject.CancelAsync(payloadDetails!);

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
        var result = await _subject.CancelAsync(payloadDetails!);

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
        _ = await _subject.CancelAsync(payloadDetails!);

        // Assert
        _httpMessageHandlerMock.AssertRequestUri(HttpMethod.Delete, $"/payload/{payloadUuid}");
    }

    [Test]
    public async Task CancelAsync_WithInvalidPayloadUuid_ShouldReturnNullAsync()
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.NotFound, "payload-notfound");

        // Act
        var result = await _subject.CancelAsync(It.IsAny<string>());

        // Assert
        Assert.IsNull(result);
    }

    [Test]
    public void CancelAsync_WithInvalidPayloadUuid_ShouldThrowExceptionAsync()
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.NotFound, "payload-notfound");

        // Act
        var ex = Assert.ThrowsAsync<HttpRequestException>(async () => await _subject.CancelAsync(It.IsAny<string>(), true));

        // Assert
        Assert.IsNotNull(ex);
        Assert.That(ex!.Message, Is.EqualTo("Error code 404, see XUMM Dev Console, reference: 'a61ba59a-0304-44ae-a86e-d74808bd5190'."));
    }
    #endregion

    #region SubscribeAsync Tests
    [Test]
    [TestCase("00000000-0000-4839-af2f-f794874a80b0")]
    public async Task SubscribeAsync_WithValidXummPayloadDetails_ShouldPassPayloadUuidAsync(string payloadUuid)
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.OK, "payload-get");

        var payloadDetails = new XummPayloadDetails
        {
            Meta = new XummPayloadDetailsMeta
            {
                Uuid = payloadUuid
            }
        };

        // Act
        await _subject.SubscribeAsync(payloadDetails, delegate (object? sender, XummSubscriptionEventArgs e)
        {
        }, It.IsAny<CancellationToken>());

        // Assert
        _xummWebSocket.Verify(x => x.SubscribeAsync(payloadUuid, It.IsAny<CancellationToken>()));
    }

    [Test]
    [TestCase("00000000-0000-4839-af2f-f794874a80b0")]
    public async Task SubscribeAsync_WithValidXummPayloadResponse_ShouldPassPayloadUuidAsync(string payloadUuid)
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.OK, "payload-get");

        var payloadDetails = new XummPayloadResponse
        {
            Uuid = payloadUuid
        };

        // Act
        await _subject.SubscribeAsync(payloadDetails, delegate (object? sender, XummSubscriptionEventArgs e)
        {
        }, It.IsAny<CancellationToken>());

        // Assert
        _xummWebSocket.Verify(x => x.SubscribeAsync(payloadUuid, It.IsAny<CancellationToken>()));
    }

    [Test]
    [TestCase("00000000-0000-4839-af2f-f794874a80b0")]
    public async Task SubscribeAsync_WithValidPayloadUuid_ShouldRaiseAllWebSocketMessagesAsync(string payloadUuid)
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.OK, "payload-get");

        var eventArgs = new List<XummSubscriptionEventArgs>();

        // Act
        await _subject.SubscribeAsync(payloadUuid, delegate (object? sender, XummSubscriptionEventArgs e)
           {
               eventArgs.Add(e);
           }, It.IsAny<CancellationToken>());

        // Assert
        Assert.AreEqual(4, eventArgs.Count);
    }

    [Test]
    public async Task SubscribeAsync_WithInvalidPayloadUuid_ShouldRaiseNoWebSocketMessagesAsync()
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.InternalServerError, "payload-error");

        var eventArgs = new List<XummSubscriptionEventArgs>();

        // Act
        await _subject.SubscribeAsync(It.IsAny<string>(), delegate (object? sender, XummSubscriptionEventArgs e)
            {
                eventArgs.Add(e);
            }, It.IsAny<CancellationToken>());

        // Assert
        Assert.IsEmpty(eventArgs);
    }
    #endregion

    public static async IAsyncEnumerable<string> GetWebSocketMessagesAsync()
    {
        yield return "{\"message\": \"Welcome aaaaaaaa-dddd-ffff-cccc-8207bd724e45\"}";
        yield return "{\"expires_in_seconds\": 30000}";
        yield return "{\"opened\": true}";
        yield return "{\"payload_uuidv4\":\"aaaaaaaa-dddd-ffff-cccc-8207bd724e45\",\"reference_call_uuidv4\":\"bbbbbbbb-eeee-aaaa-1111-8d192bd91f07\"," +
            "\"signed\":false,\"user_token\":true,\"return_url\":{\"app\":null,\"web\":null},\"custom_meta\":{}}";

        await Task.CompletedTask;
    }
}
