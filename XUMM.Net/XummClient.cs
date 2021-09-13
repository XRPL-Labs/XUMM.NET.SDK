using XUMM.Net.Authentication;

namespace XUMM.Net
{
    public class XummClient
    {
        public ApiCredentials Credentials { get; private set; }

        public XummClient(string apiKey, string apiSecret)
        {
            Credentials = new ApiCredentials(apiKey, apiSecret);
        }
    }
}
