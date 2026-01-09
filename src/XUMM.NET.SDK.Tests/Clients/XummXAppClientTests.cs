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
using XUMM.NET.SDK.Extensions;
using XUMM.NET.SDK.Models.XApp;
using XUMM.NET.SDK.Tests.Extensions;
using XUMM.NET.SDK.Tests.Fixtures;

namespace XUMM.NET.SDK.Tests.Clients;

[TestFixture]
public class XummXAppClientTests
{
    private Mock<XummHttpClient> _xummHttpClient = default!;
    private Mock<HttpMessageHandler> _httpMessageHandlerMock = default!;
    private Mock<IHttpClientFactory> _httpClientFactory = default!;
    private XummXAppClient _subject = default!;

    [SetUp]
    public void SetUp()
    {
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>();

        _httpClientFactory = new Mock<IHttpClientFactory>();
        _httpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>()))
            .Returns(new HttpClient(_httpMessageHandlerMock.Object));

        var options = Options.Create(new ApiConfig
        {
            ApiKey = "00000000-0000-0000-0000-000000000000",
            ApiSecret = "00000000-0000-0000-0000-000000000000"
        });

        _xummHttpClient = new Mock<XummHttpClient>(
            _httpClientFactory.Object,
            options,
            new Mock<ILogger<XummHttpClient>>().Object);

        _subject = new XummXAppClient(_xummHttpClient.Object, options);
    }

    [Test]
    [TestCase("1b9105dd-b7e7-456b-8303-3b9c7e48a622")]
    public async Task GetOneTimeTokenDataAsync_WithValidOneTimeToken_ShouldReturnOneTimeTokenDataAsync(string oneTimeToken)
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.OK, "xapp-get");

        // Act
        var result = await _subject.GetOneTimeTokenDataAsync(oneTimeToken);

        // Assert
        AssertExtensions.AreEqual(XAppFixtures.XummXAppOtt, result);
    }

    [Test]
    [TestCase("1b9105dd-b7e7-456b-8303-3b9c7e48a622")]
    [TestCase("f0460610-0283-42fe-b246-fb0c20a3eda4")]
    public async Task GetOneTimeTokenDataAsync_WithValidOneTimeToken_ShouldContainOneTimeTokenInRequestUriAsync(string oneTimeToken)
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.OK, "xapp-get");

        // Act
        _ = await _subject.GetOneTimeTokenDataAsync(oneTimeToken);

        // Assert
        _httpMessageHandlerMock.AssertRequestUri(HttpMethod.Get, $"xapp/ott/{oneTimeToken}");
    }

    [Test]
    [TestCase("1b9105dd-b7e7-456b-8303-3b9c7e48a622")]
    public async Task GetOneTimeTokenDataAsync_WithValidAdditionalDataKeys_ShouldReturnAdditionalDataAsync(string oneTimeToken)
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.OK, "xapp-get");

        // Act
        var result = await _subject.GetOneTimeTokenDataAsync(oneTimeToken);

        string? stringValue = null;
        string? emptyStringValue = null;
        string? nullStringValue = string.Empty;
        long int32NumberValue = 0;
        long int64NumberValue = 0;
        long negativeNumberValue = 0;

        var hasStringField = result.Origin?.Data.TryGetAdditionalDataAsString("data_string", out stringValue);
        var hasEmptyStringField = result.Origin?.Data.TryGetAdditionalDataAsString("data_empty_string", out emptyStringValue);
        var hasNullStringField = result.Origin?.Data.TryGetAdditionalDataAsString("data_null_string", out nullStringValue);
        var hasInt32Field = result.Origin?.Data.TryGetAdditionalDataAsNumber("data_number_int32", out int32NumberValue);
        var hasInt64Field = result.Origin?.Data.TryGetAdditionalDataAsNumber("data_number_int64", out int64NumberValue);
        var hasNegativeNumberField = result.Origin?.Data.TryGetAdditionalDataAsNumber("data_number_negative", out negativeNumberValue);

        // Assert
        Assert.That(hasStringField, Is.True);
        Assert.That("Test string", Is.EqualTo(stringValue));

        Assert.That(hasEmptyStringField, Is.True);
        Assert.That(string.Empty, Is.EqualTo(emptyStringValue));

        Assert.That(hasNullStringField, Is.True);
        Assert.That((string?)null, Is.EqualTo(nullStringValue));

        Assert.That(hasInt32Field, Is.True);
        Assert.That(2147483647, Is.EqualTo(int32NumberValue));

        Assert.That(hasInt64Field, Is.True);
        Assert.That(9223372036854775807, Is.EqualTo(int64NumberValue));

        Assert.That(hasNegativeNumberField, Is.True);
        Assert.That(-123, Is.EqualTo(negativeNumberValue));
    }

    [Test]
    [TestCase("1b9105dd-b7e7-456b-8303-3b9c7e48a622")]
    public async Task GetOneTimeTokenDataAsync_WithInvalidAdditionalDataKeys_ShouldNotReturnAdditionalDataAsync(string oneTimeToken)
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.OK, "xapp-get");

        // Act
        var result = await _subject.GetOneTimeTokenDataAsync(oneTimeToken);

        string? stringValue = null;
        long numberValue = 0;

        var hasStringField = result.Origin?.Data.TryGetAdditionalDataAsString("invalid_data_string", out stringValue);
        var hasNumberField = result.Origin?.Data.TryGetAdditionalDataAsNumber("invalid_data_number", out numberValue);

        // Assert
        Assert.That(hasStringField, Is.False);
        Assert.That((string?)null, Is.EqualTo(stringValue));

        Assert.That(hasNumberField, Is.False);
        Assert.That(0, Is.EqualTo(numberValue));
    }

    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    public void GetOneTimeTokenDataAsync_WithInvalidOneTimeToken_ShouldThrowExceptionAsync(string oneTimeToken)
    {
        // Act
        var ex = Assert.ThrowsAsync<ArgumentException>(() => _subject.GetOneTimeTokenDataAsync(oneTimeToken));

        // Assert
        Assert.That(ex, Is.Not.Null);
        Assert.That(ex!.Message, Is.EqualTo("Value cannot be null or white space (Parameter 'oneTimeToken')"));
    }

    [Test]
    [TestCase("abbeb856-311c-4742-b57f-c0b265404a8e", "1b9105dd-b7e7-456b-8303-3b9c7e48a622", "Device1", "7cdc3a7e20803a57c02711e25d1b92995abf9771")]
    [TestCase("fea70032-db11-4da3-a56b-91d2e409a807", "f0460610-0283-42fe-b246-fb0c20a3eda4", "Device2", "98fd8fcaff9fc32111378132c504881f04187b45")]
    public async Task ReFetchOneTimeTokenDataAsync_WithValidOneTimeTokenAndDeviceId_ShouldContainOneTimeTokenAndHashInRequestUriAsync(string apiSecret, string oneTimeToken, string deviceId, string expectedHash)
    {
        // Arrange
        var options = Options.Create(new ApiConfig
        {
            ApiKey = "00000000-0000-0000-0000-000000000000",
            ApiSecret = apiSecret
        });

        _xummHttpClient = new Mock<XummHttpClient>(
            _httpClientFactory.Object,
            options,
            new Mock<ILogger<XummHttpClient>>().Object);

        _subject = new XummXAppClient(_xummHttpClient.Object, options);

        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.OK, "xapp-get");

        // Act
        _ = await _subject.ReFetchOneTimeTokenDataAsync(oneTimeToken, deviceId);

        // Assert
        _httpMessageHandlerMock.AssertRequestUri(HttpMethod.Get, $"xapp/ott/{oneTimeToken}/{expectedHash}");
    }

    [Test]
    [TestCase("1b9105dd-b7e7-456b-8303-3b9c7e48a622", "Device1")]
    [TestCase("f0460610-0283-42fe-b246-fb0c20a3eda4", "Device2")]
    public async Task ReFetchOneTimeTokenDataAsync_WithValidOneTimeTokenAndDeviceId_ShouldReturnOneTimeTokenDataAsync(string oneTimeToken, string deviceId)
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.OK, "xapp-get");

        // Act
        var result = await _subject.ReFetchOneTimeTokenDataAsync(oneTimeToken, deviceId);

        // Assert
        AssertExtensions.AreEqual(XAppFixtures.XummXAppOtt, result);
    }

    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    public void ReFetchOneTimeTokenDataAsync_WithInvalidOneTimeToken_ShouldThrowExceptionAsync(string oneTimeToken)
    {
        // Act
        var ex = Assert.ThrowsAsync<ArgumentException>(() => _subject.ReFetchOneTimeTokenDataAsync(oneTimeToken, It.IsAny<string>()));

        // Assert
        Assert.That(ex, Is.Not.Null);
        Assert.That(ex!.Message, Is.EqualTo("Value cannot be null or white space (Parameter 'oneTimeToken')"));
    }

    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    public void ReFetchOneTimeTokenDataAsync_WithInvalidDeviceId_ShouldThrowExceptionAsync(string deviceId)
    {
        // Act
        var ex = Assert.ThrowsAsync<ArgumentException>(() => _subject.ReFetchOneTimeTokenDataAsync("1b9105dd-b7e7-456b-8303-3b9c7e48a622", deviceId));

        // Assert
        Assert.That(ex, Is.Not.Null);
        Assert.That(ex!.Message, Is.EqualTo("Value cannot be null or white space (Parameter 'deviceId')"));
    }

    [Test]
    [TestCase("e86fe076-e80a-44f9-b172-a558fdc91e38", "Test")]
    public async Task EventAsync_WithValidEventRequest_ShouldReturnEventDataAsync(string userToken, string body)
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.OK, "xapp-event");

        var request = new XummXAppEventRequest
        {
            UserToken = userToken,
            Body = body
        };

        // Act
        var result = await _subject.EventAsync(request);

        // Assert
        AssertExtensions.AreEqual(XAppFixtures.EventResponse, result);
    }

    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    public void EventAsync_WithInvalidUserToken_ShouldThrowExceptionAsync(string userToken)
    {
        // Arrange
        var request = new XummXAppEventRequest
        {
            UserToken = userToken
        };

        // Act
        var ex = Assert.ThrowsAsync<ArgumentException>(() => _subject.EventAsync(request));

        // Assert
        Assert.That(ex, Is.Not.Null);
        Assert.That(ex!.Message, Is.EqualTo("Value cannot be null or white space (Parameter 'UserToken')"));
    }

    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    public void EventAsync_WithInvalidBody_ShouldThrowExceptionAsync(string body)
    {
        // Arrange
        var request = new XummXAppEventRequest
        {
            UserToken = "e86fe076-e80a-44f9-b172-a558fdc91e38",
            Body = body
        };

        // Act
        var ex = Assert.ThrowsAsync<ArgumentException>(() => _subject.EventAsync(request));

        // Assert
        Assert.That(ex, Is.Not.Null);
        Assert.That(ex!.Message, Is.EqualTo("Value cannot be null or white space (Parameter 'Body')"));
    }

    [Test]
    [TestCase("e86fe076-e80a-44f9-b172-a558fdc91e38", "Test")]
    public async Task PushAsync_WithValidPushRequest_ShouldReturnPushDataAsync(string userToken, string body)
    {
        // Arrange
        _httpMessageHandlerMock.SetFixtureMessage(HttpStatusCode.OK, "xapp-Push");

        var request = new XummXAppPushRequest
        {
            UserToken = userToken,
            Body = body
        };

        // Act
        var result = await _subject.PushAsync(request);

        // Assert
        AssertExtensions.AreEqual(XAppFixtures.PushResponse, result);
    }

    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    public void PushAsync_WithInvalidUserToken_ShouldThrowExceptionAsync(string userToken)
    {
        // Arrange
        var request = new XummXAppPushRequest
        {
            UserToken = userToken
        };

        // Act
        var ex = Assert.ThrowsAsync<ArgumentException>(() => _subject.PushAsync(request));

        // Assert
        Assert.That(ex, Is.Not.Null);
        Assert.That(ex!.Message, Is.EqualTo("Value cannot be null or white space (Parameter 'UserToken')"));
    }

    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    public void PushAsync_WithInvalidBody_ShouldThrowExceptionAsync(string body)
    {
        // Arrange
        var request = new XummXAppPushRequest
        {
            UserToken = "e86fe076-e80a-44f9-b172-a558fdc91e38",
            Body = body
        };

        // Act
        var ex = Assert.ThrowsAsync<ArgumentException>(() => _subject.PushAsync(request));

        // Assert
        Assert.That(ex, Is.Not.Null);
        Assert.That(ex!.Message, Is.EqualTo("Value cannot be null or white space (Parameter 'Body')"));
    }
}
