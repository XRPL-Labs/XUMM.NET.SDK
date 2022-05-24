using System.Threading.Tasks;
using XUMM.NET.SDK.Webhooks;
using XUMM.NET.SDK.Webhooks.Models;

namespace XUMM.NET.WebApp.WebHooks
{
    public class XummWebhookProcessor : IXummWebhookProcessor
    {
        public Task ProcessAsync(XummWebhookBody xummWebhookBody)
        {
            // Implement your webhook processing here.
            return Task.CompletedTask;
        }
    }
}
