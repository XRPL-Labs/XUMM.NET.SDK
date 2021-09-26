using XUMM.Net.Clients.Interfaces;

namespace XUMM.Net.Clients
{
    public class XummPayloadClient : IXummPayloadClient
    {
        private readonly XummClient _xummClient;

        internal XummPayloadClient(XummClient xummClient)
        {
            _xummClient = xummClient;
        }
    }
}
