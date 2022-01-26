using Microsoft.AspNetCore.Mvc;
using XUMM.Net.ServerApp.Configs;

namespace XUMM.Net.ServerApp.Controllers;

[ApiController]
[Route("configuration")]
public class ConfigurationController : Controller
{
    private readonly IConfiguration _configuration;

    public ConfigurationController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet("api")]
    public ApiConfig GetApiConfig()
    {
        return _configuration.GetSection("Api").Get<ApiConfig>();
    }
}
