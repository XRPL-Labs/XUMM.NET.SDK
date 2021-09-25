using System.Threading.Tasks;
using XUMM.Net.Clients.Interfaces;
using XUMM.Net.Models.Misc.AppStorage;

namespace XUMM.Net.Clients
{
    public class XummClientMiscAppStorage : IXummClientMiscAppStorage
    {
        private readonly XummClient _xummClient;

        internal XummClientMiscAppStorage(XummClient xummClient)
        {
            _xummClient = xummClient;
        }

        /// <inheritdoc />
        public async Task<XummStorage> GetAsync()
        {
            return await _xummClient.GetAsync<XummStorage>("platform/app-storage");
        }
    }
}
