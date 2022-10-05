using Microsoft.Extensions.DependencyInjection;
using XUMM.NET.SDK.Clients.Interfaces;
using XUMM.NET.SDK.Configs;

namespace XUMM.NET.SDK;

/// <summary>
/// Provides API calls with a custom API Key and secret instead of using application wide credentials.
/// You can use the <see cref="XummSdk"/> if your .NET application has to connect to multiple Xumm Applications.
/// </summary>
public class XummSdk
{
    /// <summary>
    /// App storage API calls.
    /// </summary>
    public IXummMiscAppStorageClient AppStorage { get; }

    /// <summary>
    /// Miscellaneous API calls.
    /// </summary>
    public IXummMiscClient Miscellaneous { get; }

    /// <summary>
    /// Payload API calls.
    /// </summary>
    public IXummPayloadClient Payload { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="XummSdk"/> class.
    /// </summary>
    /// <param name="apiKey">API Key which can be obtained from the <see href="https://apps.xumm.dev/">Xumm Developer Console</see>.</param>
    /// <param name="apiSecret">API Secret which can be obtained from the <see href="https://apps.xumm.dev/">Xumm Developer Console</see>.</param>
    public XummSdk(string apiKey, string apiSecret) : this(ApiConfig.DefaultRestClientAddress, apiKey, apiSecret) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="XummSdk"/> class.
    /// </summary>
    /// <param name="restClientAddress">Endpoint of the Xumm API.</param>
    /// <param name="apiKey">API Key which can be obtained from the <see href="https://apps.xumm.dev/">Xumm Developer Console</see>.</param>
    /// <param name="apiSecret">API Secret which can be obtained from the <see href="https://apps.xumm.dev/">Xumm Developer Console</see>.</param>
    public XummSdk(string restClientAddress, string apiKey, string apiSecret)
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddXummNet(o =>
        {
            o.RestClientAddress = restClientAddress;
            o.ApiKey = apiKey;
            o.ApiSecret = apiSecret;
        });

        var provider = serviceCollection.BuildServiceProvider();

        AppStorage = provider.GetRequiredService<IXummMiscAppStorageClient>();
        Miscellaneous = provider.GetRequiredService<IXummMiscClient>();
        Payload = provider.GetRequiredService<IXummPayloadClient>();
    }
}
