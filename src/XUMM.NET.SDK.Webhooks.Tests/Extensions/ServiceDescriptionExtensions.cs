using Microsoft.Extensions.DependencyInjection;

namespace XUMM.NET.SDK.Webhooks.Tests.Extensions;

public static class ServiceDescriptionExtensions
{
    public static bool Is<TService, TInstance>(this ServiceDescriptor serviceDescriptor, ServiceLifetime lifetime)
    {
        return serviceDescriptor.ServiceType == typeof(TService) &&
               serviceDescriptor.ImplementationType == typeof(TInstance) &&
               serviceDescriptor.Lifetime == lifetime;
    }
}
