using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using XUMM.Net.ClientConsole.Configs;
using XUMM.Net.Models.Payload;

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

            //// Miscellaneous example calls
            //var miscellaneousConfig = config.GetSection("Miscellaneous").Get<MiscellaneousConfig>();

            //await CallAndWriteResponseAsync(client.Misc.PingAsync);
            //await CallAndWriteResponseAsync(client.Misc.GetCuratedAssetsAsync);
            //await CallAndWriteResponseAsync(() => client.Misc.GetTransactionAsync(miscellaneousConfig.TxHash));
            //await CallAndWriteResponseAsync(() => client.Misc.GetKycStatusAsync(miscellaneousConfig.Account));
            //await CallAndWriteResponseAsync(() => client.Misc.GetKycStatusAsync(miscellaneousConfig.UserToken));
            //await CallAndWriteResponseAsync(() => client.Misc.GetRatesAsync(miscellaneousConfig.CurrencyCode));
            //Console.WriteLine($"Avatar URL: {client.Misc.GetAvatarUrl(miscellaneousConfig.Account, dimensions: 200, padding: 0)}");

            //// App Storage example calls
            //await CallAndWriteResponseAsync(client.Misc.AppStorage.GetAsync);
            //await CallAndWriteResponseAsync(() => client.Misc.AppStorage.StoreAsync(miscellaneousConfig.AppStorageBody));
            //await CallAndWriteResponseAsync(client.Misc.AppStorage.ClearAsync);

            //// Payload example calls
            //var payloadConfig = config.GetSection("Payload").Get<PayloadConfig>();
            //var serializerOptions = new JsonSerializerOptions { IgnoreNullValues = true };
            //await ProcessTransaction(client, JsonSerializer.Serialize(new XummPayloadTransaction(XummTransactionType.SignIn), serializerOptions));
            //await ProcessTransaction(client, JsonSerializer.Serialize(new XrplPaymentTransaction(payloadConfig.Destination, payloadConfig.DestinationTag, payloadConfig.Fee), serializerOptions));
            //await ProcessTransaction(client, "{ \"TransactionType\": \"Payment\", \"Destination\": \"rPEPPER7kfTD9w2To4CQk6UCfuHM9c6GDY\", \"DestinationTag\": 495, \"Amount\": \"1337\" }");

            // xApp example calls
            var xAppConfig = config.GetSection("xApp").Get<XAppConfig>();
            await CallAndWriteResponseAsync(() => client.xApps.GetAsync(xAppConfig.Token));

            Console.ReadKey();
        }

        private static async Task ProcessTransaction(XummClient client, string txJson)
        {
            var payload = new XummPayload(txJson, default)
            {
                CustomMeta = new XummPayloadCustomMeta
                {
                    Instruction = "Test payload created with the XUMM.Net Wrapper."
                }
            };

            var payloadResult = await CallAndWriteResponseAsync(() => client.Payload.SubmitAsync(payload));
            await CallAndWriteResponseAsync(() => client.Payload.GetAsync(payloadResult.Uuid));
        }

        private static async Task<T> CallAndWriteResponseAsync<T>(Func<Task<T>> task)
        {
            var start = DateTime.UtcNow;
            var result = await task();

            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions { WriteIndented = true }));
            Console.WriteLine($"Response time: {Math.Round((DateTime.UtcNow - start).TotalMilliseconds)}ms.");
            return result;
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
