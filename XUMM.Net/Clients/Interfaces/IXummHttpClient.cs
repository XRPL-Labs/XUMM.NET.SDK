using System.Net.Http;
using System.Threading.Tasks;

namespace XUMM.Net.Clients.Interfaces;

public interface IXummHttpClient
{
    Task<T> GetAsync<T>(string endpoint, bool isPublicEndpoint = false);
    Task<T> PostAsync<T>(string endpoint, object content);
    Task<T> PostAsync<T>(string endpoint, string json);
    Task<T> DeleteAsync<T>(string endpoint);
    HttpClient GetHttpClient(bool setCredentials);
}
