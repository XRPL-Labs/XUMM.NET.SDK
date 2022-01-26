using System.Threading.Tasks;
using XUMM.Net.Models.XApp;

namespace XUMM.Net.Clients.Interfaces;

public interface IXummXAppClient
{
    /// <summary>
    /// This 'ott' (one time token) endpoint allows the xApp backend to retrieve verified session related information from the
    /// XUMM user.
    /// </summary>
    Task<XummXAppOttResponse> GetAsync(string token);
}
