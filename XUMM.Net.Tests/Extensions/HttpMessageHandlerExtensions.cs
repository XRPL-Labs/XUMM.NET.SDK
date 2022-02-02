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
    internal static void SetFixtureMessage(this Mock<HttpMessageHandler> httpMessageHandler,
        HttpStatusCode statusCode,
        string fixture)
    {
        var directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        if (directoryName == null)
        {
            throw new DirectoryNotFoundException("Can't get the directory name of the executing assembly.");
        }

        var fixtureFile = Path.Combine(directoryName, "Fixtures", $"{fixture}.json");
        var content = File.ReadAllText(fixtureFile);

        httpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = new StringContent(content)
            })
            .Verifiable();
    }
}
