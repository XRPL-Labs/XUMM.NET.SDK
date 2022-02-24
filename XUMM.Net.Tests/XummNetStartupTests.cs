using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using XUMM.Net.Clients;
using XUMM.Net.Clients.Interfaces;
using XUMM.Net.Tests.Extensions;

namespace XUMM.Net.Tests
{
    internal class XummNetStartupTests
    {
        private Mock<IServiceCollection> _serviceCollectionMock = default!;
        private Mock<IConfiguration> _configurationMock = default!;
        private Mock<IConfigurationSection> _configurationSectionMock = default!;

        [SetUp]
        public void SetUp()
        {
            _serviceCollectionMock = new Mock<IServiceCollection>();

            // TODO: Add unit test to test invalid appsettings values because no exception will be thrown when invalid or no ApiKey/ApiSecret are provided
            var apiConfig = "{\"RestClientAddress\":\"https://xumm.asdfapp/api/v1\",\"ApiKey\":\"00000000-0000-0000-000-000000000000\",\"ApiSecret\":\"00000000-0000-0000-000-000000000000\"}";
            _configurationSectionMock = new Mock<IConfigurationSection>();
            _configurationSectionMock.Setup(x => x.Value).Returns(apiConfig);

            _configurationMock = new Mock<IConfiguration>();
            _configurationMock.Setup(x => x.GetSection(It.Is<string>(k => k == "Xumm"))).Returns(_configurationSectionMock.Object);
        }

        [Test]
        public void WhenXummNetIsAdded_ShouldAddXummMiscAppStorageClient()
        {
            // Act
            _serviceCollectionMock.Object.AddXummNet(_configurationMock.Object);

            // Assert
            _serviceCollectionMock.Verify(sc => sc.Add(
              It.Is<ServiceDescriptor>(x => x.Is<IXummMiscAppStorageClient, XummMiscAppStorageClient>(ServiceLifetime.Singleton))));
        }

        [Test]
        public void WhenXummNetIsAdded_ShouldAddXummMiscClient()
        {
            // Act
            _serviceCollectionMock.Object.AddXummNet(_configurationMock.Object);

            // Assert
            _serviceCollectionMock.Verify(sc => sc.Add(
              It.Is<ServiceDescriptor>(x => x.Is<IXummMiscClient, XummMiscClient>(ServiceLifetime.Singleton))));
        }

        [Test]
        public void WhenXummNetIsAdded_ShouldAddXummPayloadClient()
        {
            // Act
            _serviceCollectionMock.Object.AddXummNet(_configurationMock.Object);

            // Assert
            _serviceCollectionMock.Verify(sc => sc.Add(
              It.Is<ServiceDescriptor>(x => x.Is<IXummPayloadClient, XummPayloadClient>(ServiceLifetime.Singleton))));
        }

        [Test]
        public void WhenXummNetIsAdded_ShouldAddXummHttpClient()
        {
            // Act
            _serviceCollectionMock.Object.AddXummNet(_configurationMock.Object);

            // Assert
            _serviceCollectionMock.Verify(sc => sc.Add(
              It.Is<ServiceDescriptor>(x => x.Is<IXummHttpClient, XummHttpClient>(ServiceLifetime.Singleton))));
        }
    }
}
