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

    #region CreateAsync Tests
    [Test]
    public async Task WhenSimplePaymentIsRequested_ShouldCreatePayloadAsync()
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.OK, "payload-create");
        var payload = new XummPostJsonPayload(PayloadFixtures.ValidPayloadJson);

        // Act
        var result = await _xummPayloadClient.CreateAsync(payload);

        // Assert
        AssertExtensions.AreEqual(PayloadFixtures.XummCreatePayload, result!);
    }

    [Test]
    public async Task WhenSimplePaymentIsRequestedWithJsonDocument_ShouldCreatePayloadAsync()
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
    public async Task WhenPaymentIsRequestedWithInvalidPayload_ShouldReturnNullAsync()
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.InternalServerError, "payload-error");

        // Act
        var result = await _xummPayloadClient.CreateAsync(It.IsAny<XummPostJsonPayload>(), false);

        // Assert
        Assert.IsNull(result);
    }

    [Test]
    public void WhenPaymentIsRequestedWithInvalidPayload_ShouldThrowExceptionAsync()
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

    #region CancelAsync Tests
    [Test]
    [TestCase("00000000-0000-4839-af2f-f794874a80b0")]
    public async Task WhenCancelIsRequested_ShouldCancelPayloadAsync(string payloadUuid)
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.OK, "payload-cancel");

        // Act
        var result = await _xummPayloadClient.CancelAsync(payloadUuid);

        // Assert
        _httpMessageHandlerMock.VerifyRequestUri(HttpMethod.Delete, $"/payload/{payloadUuid}");
        AssertExtensions.AreEqual(PayloadFixtures.XummDeletePayload, result!);
    }

    [Test]
    [TestCase("00000000-0000-4839-af2f-f794874a80b0")]
    public async Task WhenCancelIsRequestedWithCreatedPayload_ShouldCancelPayloadAsync(string payloadUuid)
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
        _httpMessageHandlerMock.VerifyRequestUri(HttpMethod.Delete, $"/payload/{payloadUuid}");
        AssertExtensions.AreEqual(PayloadFixtures.XummDeletePayload, result!);
    }

    [Test]
    [TestCase("00000000-0000-4839-af2f-f794874a80b0")]
    public async Task WhenCancelIsRequestedWithFetchedPayload_ShouldCancelPayloadAsync(string payloadUuid)
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
        _httpMessageHandlerMock.VerifyRequestUri(HttpMethod.Delete, $"/payload/{payloadUuid}");
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
    #endregion
}
