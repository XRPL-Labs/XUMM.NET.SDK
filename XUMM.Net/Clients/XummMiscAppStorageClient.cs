using System.Threading.Tasks;
using XUMM.Net.Clients.Interfaces;
using XUMM.Net.Models.Misc.AppStorage;

namespace XUMM.Net.Clients
{
    public class XummMiscAppStorageClient : IXummMiscAppStorageClient
    {
        private readonly XummClient _xummClient;

        internal XummMiscAppStorageClient(XummClient xummClient)
        {
            _xummClient = xummClient;
        }

        /// <inheritdoc />
        public async Task<XummStorage> GetAsync()
        {
            return await _xummClient.GetAsync<XummStorage>("app-storage");
        }

        /// <inheritdoc />
        public async Task<XummStorageStore> StoreAsync(string json)
        {
            return await _xummClient.PostAsync<XummStorageStore>("app-storage", json);
        }

        /// <inheritdoc />
        public async Task<XummStorageStore> ClearAsync()
        {
            return await _xummClient.DeleteAsync<XummStorageStore>("app-storage");
        }
    }
}
