using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using XUMM.NET.SDK;
using XUMM.NET.SDK.Clients.Interfaces;
using XUMM.NET.SDK.Models.Misc;

namespace XUMM.NET.WebApp.Pages
{
    public class PingPongModel : PageModel
    {
        public XummPong? Pong { get; set; }

        [BindProperty]
        public string ApiKey { get; set; } = default!;

        [BindProperty]
        public string ApiSecret { get; set; } = default!;

        public string? ErrorMessage { get; set; }

        private readonly IXummMiscClient _miscClient;

        public PingPongModel(IXummMiscClient miscClient)
        {
            _miscClient = miscClient;
        }

        public async Task OnPostAsync()
        {
            try
            {
                var client = !string.IsNullOrWhiteSpace(ApiKey) && !string.IsNullOrWhiteSpace(ApiSecret) ? new XummSdk(ApiKey, ApiSecret).Miscellaneous : _miscClient;
                Pong = await client.GetPingAsync();
                ErrorMessage = null;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
