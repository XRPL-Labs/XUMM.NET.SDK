using System.Threading.Tasks;
using XUMM.NET.SDK.Models.Misc.AppStorage;

namespace XUMM.NET.SDK.Clients.Interfaces;

public interface IXummMiscAppStorageClient
{
    /// <summary>
    /// Retrieve simple JSON objects attached to your XUMM App
    /// </summary>
    Task<XummStorage> GetAsync();

    /// <summary>
    /// Save simple JSON objects attached to your XUMM App
    /// </summary>
    /// <param name="json">JSON body (max 60KB) to store (attach to your XUMM application)</param>
    Task<XummStorageStore> StoreAsync(string json);

    /// <summary>
    /// Remove simple JSON objects attached to your XUMM App
    /// </summary>
    Task<XummStorageStore> ClearAsync();
}
