using System.Threading.Tasks;
using XUMM.Net.Clients.Interfaces;
using XUMM.Net.Models.Misc;

namespace XUMM.Net.Clients
{
    public class XummClientMisc : IXummClientMisc
    {
        private readonly XummClient _xummClient;

        internal XummClientMisc(XummClient xummClient)
        {
            _xummClient = xummClient;
        }

        public async Task<XummPong> PingAsync()
        {
            return await _xummClient.ReadResponseAsync<XummPong>("platform/ping");
        }
    }
}
