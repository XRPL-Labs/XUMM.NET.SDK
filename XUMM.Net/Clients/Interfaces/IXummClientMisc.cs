using System.Threading.Tasks;
using XUMM.Net.Models.Misc;

namespace XUMM.Net.Clients.Interfaces
{
    public interface IXummClientMisc
    {
        Task<XummPong> PingAsync();
    }
}
