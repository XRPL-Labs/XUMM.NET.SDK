using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;

namespace XUMM.Net.Tests.Extensions;

internal static class HttpMessageHandlerExtensions
{
    internal static void SetFixtureMessage(this Mock<HttpMessageHandler> httpMessageHandler, HttpStatusCode statusCode, string fixture)
    {
        var directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var fixtureFile = Path.Combine(directoryName!, "Data", $"{fixture}.json");

        httpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = new StringContent(File.ReadAllText(fixtureFile))
            })
            .Verifiable();
    }

    internal static void AssertRequestUri(this Mock<HttpMessageHandler> httpMessageHandler, HttpMethod httpMethod, string endpoint)
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
