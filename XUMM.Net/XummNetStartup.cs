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
        services.Configure<ApiConfig>(config.GetSection(ApiConfig.DefaultSectionKey));
        services.AddSingleton<IXummMiscAppStorageClient, XummMiscAppStorageClient>();
        services.AddSingleton<IXummMiscClient, XummMiscClient>();
        services.AddSingleton<IXummPayloadClient, XummPayloadClient>();
        services.AddSingleton<IXummHttpClient, XummHttpClient>();
        services.AddHttpClient();
        return services;
    }
}
