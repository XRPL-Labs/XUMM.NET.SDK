using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using XUMM.NET.SDK.Webhooks.Configs;
using XUMM.NET.SDK.Webhooks.Models;

namespace XUMM.NET.SDK.Webhooks.Controllers;

public class XummWebhookController : Controller
{
    private readonly ILogger<XummWebhookController> _logger;
    private readonly IXummWebhookProcessor _xummWebhookProcessor;
    private readonly XummWebhookConfig _config;

    public XummWebhookController(
        IXummWebhookProcessor xummWebhookProcessor,
        ILogger<XummWebhookController> logger)
        : this(xummWebhookProcessor, Options.Create(new XummWebhookConfig()), logger)
    {
    }

    /// <summary>
    /// Creates the controller. When <see cref="XummWebhookConfig.ApiSecret" /> is set, webhooks without a valid
    /// <c>X-Xumm-Request-Signature</c> are rejected with 401 Unauthorized before they reach the processor.
    /// </summary>
    [ActivatorUtilitiesConstructor]
    public XummWebhookController(
        IXummWebhookProcessor xummWebhookProcessor,
        IOptions<XummWebhookConfig> config,
        ILogger<XummWebhookController> logger)
    {
        _xummWebhookProcessor = xummWebhookProcessor;
        _config = config.Value;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> ProcessAsync()
    {
        try
        {
            using var body = new MemoryStream();
            await HttpContext.Request.Body.CopyToAsync(body);
            var bodyBytes = body.ToArray();

            if (!string.IsNullOrEmpty(_config.ApiSecret) &&
                !XummWebhookSignature.IsValid(
                    _config.ApiSecret!,
                    HttpContext.Request.Headers[XummWebhookSignature.TimestampHeader],
                    HttpContext.Request.Headers[XummWebhookSignature.SignatureHeader],
                    bodyBytes))
            {
                _logger.LogWarning("Rejected a Payload webhook with a missing or invalid signature.");
                return Unauthorized();
            }

            var result = JsonSerializer.Deserialize<XummWebhookBody>(bodyBytes);
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
