using System.Threading.Tasks;
using XUMM.Net.Models.Misc.AppStorage;

namespace XUMM.Net.Clients.Interfaces
{
    public interface IXummClientMiscAppStorage
    {
        /// <summary>
        /// Retrieve simple JSON objects attached to your XUMM App
        /// </summary>
        Task<XummStorage> GetAsync();
    }
}
