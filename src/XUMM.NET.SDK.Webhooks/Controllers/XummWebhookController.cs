using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using XUMM.NET.SDK.Webhooks.Models;

namespace XUMM.NET.SDK.Webhooks.Controllers;

public class XummWebhookController : Controller
{
    private readonly ILogger<XummWebhookController> _logger;
    private readonly IXummWebhookProcessor _xummWebhookProcessor;

    public XummWebhookController(
        IXummWebhookProcessor xummWebhookProcessor,
        ILogger<XummWebhookController> logger)
    {
        _xummWebhookProcessor = xummWebhookProcessor;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> ProcessAsync()
    {
        try
        {
            using var streamReader = new StreamReader(HttpContext.Request.Body);
            var responseText = await streamReader.ReadToEndAsync();

            var result = JsonSerializer.Deserialize<XummWebhookBody>(responseText);
            if (result == null)
            {
                throw new Exception("Unexpected response for a Payload webhook.");
            }

            await _xummWebhookProcessor.ProcessAsync(result);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected response for a Payload webhook.");
            return BadRequest();
        }
    }
}
