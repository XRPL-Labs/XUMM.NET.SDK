using Microsoft.Extensions.Caching.Memory;
using XUMM.NET.SDK.Webhooks;
using XUMM.NET.SDK.Webhooks.Models;

namespace XUMM.NET.ServerApp.Webhooks;

public class XummWebhookProcessor : IXummWebhookProcessor
{
    private readonly IMemoryCache _memoryCache;

    public XummWebhookProcessor(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public Task ProcessAsync(XummWebhookBody xummWebhookBody)
    {
        if (xummWebhookBody.UserToken != null)
        {
            var options = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.FromUnixTimeSeconds(xummWebhookBody.UserToken.TokenExpiration)
            };

            _memoryCache.Set("USER_TOKEN", xummWebhookBody.UserToken.UserToken, options);
        }

        return Task.CompletedTask;
    }
}
