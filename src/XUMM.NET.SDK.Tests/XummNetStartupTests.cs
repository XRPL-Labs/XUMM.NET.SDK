using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using XUMM.NET.SDK.Clients;
using XUMM.NET.SDK.Clients.Interfaces;
using XUMM.NET.SDK.Tests.Extensions;
using XUMM.NET.SDK.WebSocket;

namespace XUMM.NET.SDK.Tests
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

            // TODO: Add unit test to test invalid appsettings values because no exception will be thrown when invalid or no ApiKey/ApiSecret are set
            //       The setter is being called after the first use of the ApiConfig.
            var apiConfig = "{\"RestClientAddress\":\"https://xumm.app/api/v1\",\"ApiKey\":\"00000000-0000-0000-000-000000000000\",\"ApiSecret\":\"00000000-0000-0000-000-000000000000\"}";
            _configurationSectionMock = new Mock<IConfigurationSection>();
            _configurationSectionMock.Setup(x => x.Value).Returns(apiConfig);

            _configurationMock = new Mock<IConfiguration>();
            _configurationMock.Setup(x => x.GetSection(It.Is<string>(k => k == "Xumm"))).Returns(_configurationSectionMock.Object);
        }

        [Test]
        public void WhenXummNetIsAddedWithDefaultConfigurationSection_ShouldAddXummClients()
        {
            // Arrange 
            _configurationMock.Setup(x => x.GetSection(It.Is<string>(k => k == "Xumm"))).Returns(_configurationSectionMock.Object);

            // Act
            _serviceCollectionMock.Object.AddXummNet(_configurationMock.Object);

            // Assert
            _serviceCollectionMock.Verify(sc => sc.Add(
                It.Is<ServiceDescriptor>(x => x.Is<IXummMiscAppStorageClient, XummMiscAppStorageClient>(ServiceLifetime.Singleton))));
            _serviceCollectionMock.Verify(sc => sc.Add(
                It.Is<ServiceDescriptor>(x => x.Is<IXummMiscClient, XummMiscClient>(ServiceLifetime.Singleton))));
            _serviceCollectionMock.Verify(sc => sc.Add(
                It.Is<ServiceDescriptor>(x => x.Is<IXummPayloadClient, XummPayloadClient>(ServiceLifetime.Singleton))));
            _serviceCollectionMock.Verify(sc => sc.Add(
                It.Is<ServiceDescriptor>(x => x.Is<IXummXAppClient, XummXAppClient>(ServiceLifetime.Singleton))));
            _serviceCollectionMock.Verify(sc => sc.Add(
                It.Is<ServiceDescriptor>(x => x.Is<IXummWebSocket, XummWebSocket>(ServiceLifetime.Transient))));
            _serviceCollectionMock.Verify(sc => sc.Add(
                It.Is<ServiceDescriptor>(x => x.Is<IXummHttpClient, XummHttpClient>(ServiceLifetime.Singleton))));
        }

        [Test]
        public void WhenXummNetIsAddedWithManualConfiguration_ShouldAddXummClients()
        {
            // Act
            _serviceCollectionMock.Object.AddXummNet(o =>
            {
                o.ApiKey = "00000000-0000-0000-000-000000000000";
                o.ApiSecret = "00000000-0000-0000-000-000000000000";
            });

            // Assert
            _serviceCollectionMock.Verify(sc => sc.Add(
                It.Is<ServiceDescriptor>(x => x.Is<IXummWebSocket, XummWebSocket>(ServiceLifetime.Transient))));
            _serviceCollectionMock.Verify(sc => sc.Add(
               It.Is<ServiceDescriptor>(x => x.Is<IXummMiscAppStorageClient, XummMiscAppStorageClient>(ServiceLifetime.Singleton))));
            _serviceCollectionMock.Verify(sc => sc.Add(
                It.Is<ServiceDescriptor>(x => x.Is<IXummMiscClient, XummMiscClient>(ServiceLifetime.Singleton))));
            _serviceCollectionMock.Verify(sc => sc.Add(
                It.Is<ServiceDescriptor>(x => x.Is<IXummPayloadClient, XummPayloadClient>(ServiceLifetime.Singleton))));
            _serviceCollectionMock.Verify(sc => sc.Add(
                It.Is<ServiceDescriptor>(x => x.Is<IXummXAppClient, XummXAppClient>(ServiceLifetime.Singleton))));
            _serviceCollectionMock.Verify(sc => sc.Add(
                It.Is<ServiceDescriptor>(x => x.Is<IXummHttpClient, XummHttpClient>(ServiceLifetime.Singleton))));
        }

        [Test]
        public void WhenXummNetIsAddedWithoutSectionFound_ShouldThrowException()
        {
            // Arrange 
            _configurationMock.Setup(x => x.GetSection(It.Is<string>(k => k == "Xumm"))).Returns<IConfigurationSection?>(null);

            // Act
            var ex = Assert.Throws<Exception>(() => _serviceCollectionMock.Object.AddXummNet(_configurationMock.Object));

            // Assert
            Assert.That(ex, Is.Not.Null);
            Assert.That(ex!.Message, Is.EqualTo("Failed to find configuration section with key 'Xumm'"));
        }
    }
}
