using System.Threading.Tasks;
using XUMM.Net.Models.Payload;

namespace XUMM.Net.Clients.Interfaces
{
    public interface IXummPayloadClient
    {
        /// <summary>
        /// Submit a payload containing a sign request to the XUMM platform.
        /// </summary>
        Task<XummPayloadResponse> SubmitAsync(XummPayload payload);
    }
}
