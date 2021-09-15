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

        /// <summary>
        /// Fetch transaction & outcome live from XRP ledger full history nodes (through the XUMM platform) containing parsed transaction outcome balance mutations
        /// </summary>
        /// <param name="txHash">The transaction hash (64 hexadecimal characters)</param>
        Task<XummTransaction> GetTransactionAsync(string txHash);
    }
}
