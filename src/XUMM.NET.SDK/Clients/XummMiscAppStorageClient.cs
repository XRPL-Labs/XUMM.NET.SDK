using System.Threading.Tasks;
using XUMM.Net.Clients.Interfaces;
using XUMM.Net.Models.Misc.AppStorage;

namespace XUMM.Net.Clients;

public class XummMiscAppStorageClient : IXummMiscAppStorageClient
{
    private readonly IXummHttpClient _httpClient;

    public XummMiscAppStorageClient(IXummHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <inheritdoc />
    public async Task<XummStorage> GetAsync()
    {
        return await _httpClient.GetAsync<XummStorage>("app-storage");
    }

    /// <inheritdoc />
    public async Task<XummStorageStore> StoreAsync(string json)
    {
        return await _httpClient.PostAsync<XummStorageStore>("app-storage", json);
    }

    /// <inheritdoc />
    public async Task<XummStorageStore> ClearAsync()
    {
        return await _httpClient.DeleteAsync<XummStorageStore>("app-storage");
    }
}
