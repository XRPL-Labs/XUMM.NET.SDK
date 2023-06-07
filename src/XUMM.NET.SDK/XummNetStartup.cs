using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XUMM.NET.SDK.Clients;
using XUMM.NET.SDK.Clients.Interfaces;
using XUMM.NET.SDK.Configs;
using XUMM.NET.SDK.WebSocket;

namespace XUMM.NET.SDK;

public static class XummNetStartup
{
    public static IServiceCollection AddXummNet(this IServiceCollection services, IConfiguration config)
    {
        var section = config.GetSection(ApiConfig.DefaultSectionKey);
        if (!section.Exists())
        {
            throw new Exception($"Failed to find configuration section with key '{ApiConfig.DefaultSectionKey}'");
        }

        services.Configure<ApiConfig>(section);
        services.AddXummNetClients();
        return services;
    }

    public static IServiceCollection AddXummNet(this IServiceCollection services, Action<ApiConfig> configureOptions)
    {
        services.Configure(configureOptions);
        services.AddXummNetClients();
        return services;
    }

    private static IServiceCollection AddXummNetClients(this IServiceCollection services)
    {
        services.AddTransient<IXummWebSocket, XummWebSocket>();
        services.AddSingleton<IXummMiscAppStorageClient, XummMiscAppStorageClient>();
        services.AddSingleton<IXummMiscClient, XummMiscClient>();
        services.AddSingleton<IXummPayloadClient, XummPayloadClient>();
        services.AddSingleton<IXummXAppClient, XummXAppClient>();
        services.AddSingleton<IXummXAppJwtClient, XummXAppJwtClient>();
        services.AddSingleton<IXummHttpClient, XummHttpClient>();
        services.AddHttpClient();
        return services;
    }
}
