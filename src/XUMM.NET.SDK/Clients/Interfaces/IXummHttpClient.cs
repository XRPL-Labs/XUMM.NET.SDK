using System.Net.Http;
using System.Threading.Tasks;

namespace XUMM.NET.SDK.Clients.Interfaces;

public interface IXummHttpClient
{
    Task<T> GetAsync<T>(HttpClient client, string endpoint);
    Task<T> GetAsync<T>(string endpoint);
    Task<T> GetPublicAsync<T>(string endpoint);
    Task<T> PostAsync<T>(string endpoint, object content);
    Task<T> PostAsync<T>(string endpoint, string json);
    Task<T> PostAsync<T>(HttpClient client, string endpoint, string json);
    Task<T> DeleteAsync<T>(string endpoint);
    Task<T> DeleteAsync<T>(HttpClient client, string endpoint);
    HttpClient GetHttpClient(bool setCredentials);
}
