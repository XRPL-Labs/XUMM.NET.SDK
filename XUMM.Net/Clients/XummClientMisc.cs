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

        /// <inheritdoc cref="IXummClientMisc.PingAsync"/>
        public async Task<XummPong> PingAsync()
        {
            return await _xummClient.GetAsync<XummPong>("platform/ping");
        }

        /// <inheritdoc cref="IXummClientMisc.CuratedAssetsAsync"/>
        public async Task<XummCuratedAssets> CuratedAssetsAsync()
        {
            return await _xummClient.GetAsync<XummCuratedAssets>("platform/curated-assets");
        }

        /// <inheritdoc cref="IXummClientMisc.GetTransactionAsync"/>
        public async Task<XummTransaction> GetTransactionAsync(string txHash)
        {
            return await _xummClient.GetAsync<XummTransaction>($"platform/xrpl-tx/{txHash.Trim()}");
        }
    }
}
