using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using Moq;
using Moq.Protected;
using NUnit.Framework;

namespace XUMM.Net.Tests.Extensions;

internal static class AssertExtensions
{
    internal static void AreEqual(object expected, object actual)
    {
        var expectedJson = JsonSerializer.Serialize(expected);
        var actualJson = JsonSerializer.Serialize(actual);
        Assert.AreEqual(expectedJson, actualJson);
    }

    internal static void VerifyRequestUri(this Mock<HttpMessageHandler> httpMessageHandler, HttpMethod httpMethod, string endpoint)
    {
        httpMessageHandler
            .Protected()
            .Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(request =>
                request.Method == httpMethod && 
                request.RequestUri!.ToString().EndsWith(endpoint, StringComparison.Ordinal)),
                ItExpr.IsAny<CancellationToken>());
    }
}
