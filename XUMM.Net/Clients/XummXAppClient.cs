using System.Threading.Tasks;
using XUMM.Net.Clients.Interfaces;
using XUMM.Net.Models.XApp;

namespace XUMM.Net.Clients
{
    public class XummXAppClient : IXummXAppClient
    {
        private readonly XummClient _xummClient;

        internal XummXAppClient(XummClient xummClient)
        {
            _xummClient = xummClient;
        }

        /// <inheritdoc />
        public async Task<XummXAppOttResponse> GetAsync(string token)
        {
            return await _xummClient.GetAsync<XummXAppOttResponse>($"xapp/ott/{token}");
        }
    }
}
