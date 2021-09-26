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

        /// <summary>
        /// Get payload details or payload resolve status and result data
        /// </summary>
        /// <param name="payloadUuid">Payload UUID as received from the Payload POST endpoint.</param>
        Task<XummPayloadDetails> GetAsync(string payloadUuid);
    }
}
