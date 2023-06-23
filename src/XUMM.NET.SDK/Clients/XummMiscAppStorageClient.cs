using System.Threading.Tasks;
using XUMM.NET.SDK.Clients.Interfaces;
using XUMM.NET.SDK.Models.Misc.AppStorage;

namespace XUMM.NET.SDK.Clients;

/// <summary>
/// Represents the client for app storage API calls.
/// </summary>
public class XummMiscAppStorageClient : IXummMiscAppStorageClient
{
    private readonly IXummHttpClient _httpClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="XummMiscAppStorageClient"/> class.
    /// </summary>
    public XummMiscAppStorageClient(IXummHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <inheritdoc />
    public async Task<XummStorage> GetAsync()
    {
        return await _httpClient.GetAsync<XummStorage>("platform/app-storage");
    }

    /// <inheritdoc />
    public async Task<XummStorageStore> StoreAsync(string json)
    {
        return await _httpClient.PostAsync<XummStorageStore>("platform/app-storage", json);
    }

    /// <inheritdoc />
    public async Task<XummStorageStore> ClearAsync()
    {
        return await _httpClient.DeleteAsync<XummStorageStore>("platform/app-storage");
    }
}
