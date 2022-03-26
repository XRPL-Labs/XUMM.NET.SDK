using XUMM.NET.SDK.Webhooks;
using XUMM.NET.SDK.Webhooks.Models;

namespace XUMM.NET.ServerApp.Webhooks;

public class XummWebhookProcessor : IXummWebhookProcessor
{
    public Task ProcessAsync(XummWebhookBody xummWebhookBody)
    {
        // Implement your webhook processing here.
        return Task.CompletedTask;
    }
}
