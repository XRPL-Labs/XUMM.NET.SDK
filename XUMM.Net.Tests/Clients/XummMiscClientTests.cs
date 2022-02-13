using System.Collections.Generic;
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
    public async Task WhenValidCredentialsAreProvided_ShouldReturnAppNameAsync()
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.OK, "pong");

        // Act
        var result = await _xummMiscClient.PingAsync();

        // Assert
        Assert.AreEqual(true, result.Pong);
        Assert.AreEqual(0, result.Auth.Application.Disabled);
        Assert.AreEqual("SomeApplication", result.Auth.Application.Name);
        Assert.AreEqual("00000000-0000-4839-af2f-f794874a80b0", result.Auth.Application.Uuidv4);
        Assert.AreEqual("https://webhook.site/00000000-0000-4e34-8112-c4391247a8ee",
            result.Auth.Application.WebhookUrl);
        Assert.AreEqual("2904b05f-5b37-4f3e-a624-940ad817943c", result.Auth.Call.Uuidv4);
    }

    [Test]
    public async Task WhenCuratedAssetsAreRequested_ShouldReturnCuratedAssetsAsync()
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.OK, "curatedassets");

        // Act
        var result = await _xummMiscClient.GetCuratedAssetsAsync();

        // Assert
        CollectionAssert.AreEqual(new List<string>
            {
                "Bitstamp",
                "Wietse"
            },
            result.Issuers);

        CollectionAssert.AreEqual(new List<string>
            {
                "USD",
                "BTC",
                "ETH",
                "WIE"
            },
            result.Currencies);

        CollectionAssert.AreEqual(new List<string>
            {
                "WIE"
            },
            result.Details["Wietse"].Currencies.Keys);

        CollectionAssert.DoesNotContain(result.Details["Wietse"].Currencies.Keys, "USD");

        CollectionAssert.AreEqual(new List<string>
            {
                "USD",
                "BTC"
            },
            result.Details["Bitstamp"].Currencies.Keys);

        CollectionAssert.DoesNotContain(result.Details["Bitstamp"].Currencies.Keys, "WIE");
    }
}
