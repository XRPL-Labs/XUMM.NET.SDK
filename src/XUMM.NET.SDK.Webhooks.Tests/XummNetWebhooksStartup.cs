using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using XUMM.NET.SDK.Webhooks.Tests.Extensions;

namespace XUMM.NET.SDK.Webhooks.Tests;

internal class XummNetWebhooksStartup
{
    private Mock<IServiceCollection> _serviceCollectionMock = default!;

    [SetUp]
    public void SetUp()
    {
        _serviceCollectionMock = new Mock<IServiceCollection>();
    }

    [Test]
    public void WhenXummWebhooksIsAdded_ShouldImplementExampleProcessor()
    {
        // Act
        _serviceCollectionMock.Object.AddXummWebhooks<ExampleXummWebhookProcessor>();

        // Assert
        _serviceCollectionMock.Verify(sc => sc.Add(
            It.Is<ServiceDescriptor>(x =>
                x.Is<IXummWebhookProcessor, ExampleXummWebhookProcessor>(ServiceLifetime.Singleton))));
    }
}
