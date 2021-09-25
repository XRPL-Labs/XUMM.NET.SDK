using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using XUMM.Net.ClientConsole.Configs;

namespace XUMM.Net.ClientConsole
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var config = GetConfiguration();

            var apiConig = config.GetSection("Api").Get<ApiConfig>();

            //The XUMM API can be called using an API Key and API Secret, which can be obtained from the xumm Developer Dashboard.
            var credentials = new XummApiCredentials(apiConig.Key, apiConig.Secret);

            // Xumm client options with default endpoint is used here.
            var options = new XummClientOptions(credentials);

            using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            using var client = new XummClient(options, loggerFactory);

            var miscellaneousConfig = config.GetSection("Miscellaneous").Get<MiscellaneousConfig>();

            await CallAndWriteResponseAsync(client.Misc.PingAsync);
            await CallAndWriteResponseAsync(client.Misc.GetCuratedAssetsAsync);
            await CallAndWriteResponseAsync(() => client.Misc.GetTransactionAsync(miscellaneousConfig.TxHash));
            await CallAndWriteResponseAsync(() => client.Misc.GetKycStatusAsync(miscellaneousConfig.Account));
            await CallAndWriteResponseAsync(() => client.Misc.GetKycStatusAsync(miscellaneousConfig.UserToken));
            await CallAndWriteResponseAsync(() => client.Misc.GetRatesAsync(miscellaneousConfig.CurrencyCode));
            Console.WriteLine($"Avatar URL: {client.Misc.GetAvatarUrl(miscellaneousConfig.Account, dimensions: 200, padding: 0)}");

            await CallAndWriteResponseAsync(client.Misc.AppStorage.GetAsync);
            await CallAndWriteResponseAsync(() => client.Misc.AppStorage.StoreAsync(miscellaneousConfig.AppStorageBody));
            await CallAndWriteResponseAsync(client.Misc.AppStorage.ClearAsync);

            Console.ReadKey();
        }

        private static async Task CallAndWriteResponseAsync<T>(Func<Task<T>> task)
        {
            var start = DateTime.UtcNow;
            var result = await task();

            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions { WriteIndented = true }));
            Console.WriteLine($"Response time: {Math.Round((DateTime.UtcNow - start).TotalMilliseconds)}ms.");
        }

        private static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile("appsettings.Development.json", optional: true);

            return builder.Build();
        }
    }
}
