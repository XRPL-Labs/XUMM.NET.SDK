using System.Threading.Tasks;
using XUMM.NET.SDK.Webhooks.Models;

namespace XUMM.NET.SDK.Webhooks;

public interface IXummWebhookProcessor
{
    Task ProcessAsync(XummWebhookBody xummWebhookBody);
}
