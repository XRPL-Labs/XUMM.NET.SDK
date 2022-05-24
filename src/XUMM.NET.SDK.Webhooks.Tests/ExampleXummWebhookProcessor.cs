using System;
using System.Threading.Tasks;
using XUMM.NET.SDK.Webhooks.Models;

namespace XUMM.NET.SDK.Webhooks.Tests;

public class ExampleXummWebhookProcessor : IXummWebhookProcessor
{
    public Task ProcessAsync(XummWebhookBody xummWebhookBody)
    {
        throw new NotImplementedException();
    }
}
