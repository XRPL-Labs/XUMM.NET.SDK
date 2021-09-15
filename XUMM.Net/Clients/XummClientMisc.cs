using System.Threading.Tasks;
using XUMM.Net.Clients.Interfaces;
using XUMM.Net.Enums;
using XUMM.Net.Extensions;
using XUMM.Net.Helpers;
using XUMM.Net.Models.Misc;

namespace XUMM.Net.Clients
{
    public class XummClientMisc : IXummClientMisc
    {
        private readonly XummClient _xummClient;

        internal XummClientMisc(XummClient xummClient)
        {
            _xummClient = xummClient;
        }

        /// <inheritdoc />
        public async Task<XummPong> PingAsync()
        {
            return await _xummClient.GetAsync<XummPong>("platform/ping");
        }

        /// <inheritdoc />
        public async Task<XummCuratedAssets> GetCuratedAssetsAsync()
        {
            return await _xummClient.GetAsync<XummCuratedAssets>("platform/curated-assets");
        }

        /// <inheritdoc />
        public async Task<XummTransaction> GetTransactionAsync(string txHash)
        {
            return await _xummClient.GetAsync<XummTransaction>($"platform/xrpl-tx/{txHash}");
        }

        /// <inheritdoc />
        public async Task<XummKycStatus> GetKycStatusAsync(string userTokenOrAccount)
        {
            if (userTokenOrAccount.IsAccountAddress())
            {
                var kycInfo = await _xummClient.GetAsync<XummKycInfo>($"platform/kyc-status/{userTokenOrAccount}");
                return kycInfo.KycApproved ? XummKycStatus.Successful : XummKycStatus.None;
            }
            else
            {
                // TODO: Validate and extend the model of XummKycStatusInfo
                var kycInfo = await _xummClient.GetAsync<XummKycStatusInfo>($"platform/kyc-status/{userTokenOrAccount}");
                return EnumHelper.GetValueFromName<XummKycStatus>(kycInfo.KycStatus);
            }
        }
    }
}
