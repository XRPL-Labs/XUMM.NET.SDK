using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using XUMM.NET.SDK.Clients.Interfaces;
using XUMM.NET.SDK.Models.Misc;

namespace XUMM.NET.WebApp.Pages
{
    public class PingPongModel : PageModel
    {
        public XummPong Pong { get; set; }

        private readonly IXummMiscClient _miscClient;

        public PingPongModel(IXummMiscClient miscClient)
        {
            _miscClient = miscClient;
        }

        public async Task OnGetAsync()
        {
            Pong = await _miscClient.GetPingAsync();
        }
    }
}
