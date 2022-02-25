using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XUMM.Net.Clients;
using XUMM.Net.Clients.Interfaces;
using XUMM.Net.Configs;

namespace XUMM.Net;

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
        services.AddSingleton<IXummMiscAppStorageClient, XummMiscAppStorageClient>();
        services.AddSingleton<IXummMiscClient, XummMiscClient>();
        services.AddSingleton<IXummPayloadClient, XummPayloadClient>();
        services.AddSingleton<IXummHttpClient, XummHttpClient>();
        services.AddHttpClient();
        return services;
    }
}
