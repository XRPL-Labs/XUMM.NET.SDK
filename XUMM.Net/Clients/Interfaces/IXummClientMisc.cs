using System.Threading.Tasks;
using XUMM.Net.Models.Misc;

namespace XUMM.Net.Clients.Interfaces
{
    public interface IXummClientMisc
    {
        Task<XummPong> PingAsync();

        /// <summary>
        /// Get curated assets from the XUMM API. This API contains the same issuers and assets available to users in XUMM when they press the "Add asset" button on the home screen.
        /// </summary>
        Task<XummCuratedAssets> CuratedAssetsAsync();
    }
}
