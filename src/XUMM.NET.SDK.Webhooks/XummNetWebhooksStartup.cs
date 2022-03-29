using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace XUMM.NET.SDK.Webhooks;

public static class XummNetWebhooksStartup
{
    /// <summary>
    /// Adds services for the webhooks to the specified <see cref="IServiceCollection" />.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
    /// <returns>The <see cref="IServiceCollection" />.</returns>
    public static IServiceCollection AddXummWebhooks<T>(this IServiceCollection services)
        where T : IXummWebhookProcessor
    {
        services.AddSingleton(typeof(IXummWebhookProcessor), typeof(T));
        return services;
    }

#if NET5_0_OR_GREATER

    /// <summary>
    /// Adds endpoints for controller actions to the <see cref="IEndpointRouteBuilder" /> and specifies a route
    /// with the given <paramref name="pattern" />.
    /// </summary>
    /// <param name="endpoints">The <see cref="IEndpointRouteBuilder" /> to add the route to.</param>
    /// <param name="pattern">The URL pattern of the route.</param>
    /// <returns>
    /// An <see cref="ControllerActionEndpointConventionBuilder" /> for endpoints associated with controller actions for this
    /// route.
    /// </returns>
    public static IEndpointRouteBuilder MapXummControllerRoute(this IEndpointRouteBuilder endpoints,
        string pattern = "Xumm/Webhook")
    {
        endpoints.MapControllerRoute(
            typeof(IXummWebhookProcessor).FullName!,
            pattern,
            new
            {
                controller = "XummWebhook",
                action = "Process"
            });

        return endpoints;
    }

#else
    /// <summary>
    /// Adds endpoints for controller actions to the specified <see cref="IApplicationBuilder" />and specifies a route
    /// with the given <paramref name="pattern" />.
    /// </summary>
    /// <param name="builder">The <see cref="IApplicationBuilder" /> to add the middleware to.</param>
    /// <param name="pattern">The URL pattern of the route.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    public static IApplicationBuilder MapXummControllerRoute(this IApplicationBuilder builder,
        string pattern = "Xumm/Webhook")
    {
        builder.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                typeof(IXummWebhookProcessor).FullName!,
                pattern,
                new
                {
                    controller = "XummWebhook",
                    action = "Process"
                });
        });

        return builder;
    }
#endif
}
