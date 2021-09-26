using System.Threading.Tasks;
using XUMM.Net.Clients.Interfaces;
using XUMM.Net.Models.Payload;

namespace XUMM.Net.Clients
{
    public class XummPayloadClient : IXummPayloadClient
    {
        private readonly XummClient _xummClient;

        internal XummPayloadClient(XummClient xummClient)
        {
            _xummClient = xummClient;
        }

        /// <inheritdoc />
        public async Task<XummPayloadResponse> SubmitAsync(XummPayload payload)
        {
            return await _xummClient.PostAsync<XummPayloadResponse>("platform/payload", payload);
        }

        /// <inheritdoc />
        public async Task<XummPayloadDetails> GetAsync(string payloadUuid)
        {
            return await _xummClient.GetAsync<XummPayloadDetails>($"platform/payload/{payloadUuid}");
        }
    }
}
