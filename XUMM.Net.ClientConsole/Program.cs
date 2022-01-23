using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using XUMM.Net.ClientConsole.Configs;
using XUMM.Net.Enums;
using XUMM.Net.EventArgs;
using XUMM.Net.Models.Payload;
using XUMM.Net.Models.Payload.XRPL;
using XUMM.Net.Models.Payload.Xumm;

namespace XUMM.Net.ClientConsole
{
    public class Program
    {
        static async Task Main()
        {
            var config = GetConfiguration();

            var apiConig = config.GetSection("Api").Get<ApiConfig>();

            //The XUMM API can be called using an API Key and API Secret, which can be obtained from the xumm Developer Dashboard.
            var credentials = new XummApiCredentials(apiConig.Key, apiConig.Secret);

            // Xumm client options with default endpoint is used here.
            var options = new XummClientOptions(credentials);

            using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            using var client = new XummClient(options, loggerFactory);

            // Miscellaneous example calls
            var miscellaneousConfig = config.GetSection("Miscellaneous").Get<MiscellaneousConfig>();

            await CallAndWriteResponseAsync(client.Misc.PingAsync);
            await CallAndWriteResponseAsync(client.Misc.GetCuratedAssetsAsync);
            await CallAndWriteResponseAsync(() => client.Misc.GetTransactionAsync(miscellaneousConfig.TxHash));
            await CallAndWriteResponseAsync(() => client.Misc.GetKycStatusAsync(miscellaneousConfig.Account));
            await CallAndWriteResponseAsync(() => client.Misc.GetKycStatusAsync(miscellaneousConfig.UserToken));
            await CallAndWriteResponseAsync(() => client.Misc.GetRatesAsync(miscellaneousConfig.CurrencyCode));
            Console.WriteLine($"Avatar URL: {client.Misc.GetAvatarUrl(miscellaneousConfig.Account, dimensions: 200, padding: 0)}");

            // App Storage example calls
            await CallAndWriteResponseAsync(client.Misc.AppStorage.GetAsync);
            await CallAndWriteResponseAsync(() => client.Misc.AppStorage.StoreAsync(miscellaneousConfig.AppStorageBody));
            await CallAndWriteResponseAsync(client.Misc.AppStorage.ClearAsync);

            //Payload example calls
            var payloadConfig = config.GetSection("Payload").Get<PayloadConfig>();
            var serializerOptions = new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
            await ProcessTransactionAsync(client, JsonSerializer.Serialize(new XrplPaymentTransaction(payloadConfig.Destination, payloadConfig.DestinationTag, payloadConfig.Fee), serializerOptions));
            await ProcessTransactionAsync(client, "{ \"TransactionType\": \"Payment\", \"Destination\": \"rPEPPER7kfTD9w2To4CQk6UCfuHM9c6GDY\", \"DestinationTag\": 495, \"Amount\": \"1337\" }");
            await CreatePayloadAndSubscribeAsync(client, payloadConfig, JsonSerializer.Serialize(new XummPayloadTransaction(XummTransactionType.SignIn), serializerOptions));

            //// xApp example callsA task was canceled.'
            //var xAppConfig = config.GetSection("xApp").Get<XAppConfig>();
            //await CallAndWriteResponseAsync(() => client.xApps.GetAsync(xAppConfig.Token));
            Console.WriteLine("Done");
            Console.ReadKey();
        }

        private static async Task CreatePayloadAndSubscribeAsync(XummClient client, PayloadConfig config, string txJson)
        {
            var payloadResult = await GetPayloadAsync(client, txJson);

            if (config.OpenSubscriptionQrCodeInBrowser)
            {
                var ps = new ProcessStartInfo
                {
                    FileName = payloadResult.Refs.QrPng,
                    UseShellExecute = true
                };

                Process.Start(ps);
            }

            await client.Payload.SubscribeAsync(payloadResult.Uuid, Subscription_EventArgs);
        }

        static void Subscription_EventArgs(object sender, XummSubscriptionEventArgs e)
        {
            if (e.Data.RootElement.TryGetProperty("message", out var messageElement))
            {
                Console.WriteLine("Connected: {0}", messageElement.GetString());
            }
            else if (e.Data.RootElement.TryGetProperty("expires_in_seconds", out var expiresElement))
            {
                var ts = TimeSpan.FromSeconds(expiresElement.GetInt32());
                Console.WriteLine("Expires in {0}", ts);
            }
            else if (e.Data.RootElement.TryGetProperty("signed", out var payloadElement))
            {
                Console.WriteLine("Signed: {0}", payloadElement.GetBoolean() ? "Yes" : "No");
                e.CloseConnectionAsync();
            }
        }

        private static async Task ProcessTransactionAsync(XummClient client, string txJson)
        {
            var payloadResult = await GetPayloadAsync(client, txJson);
            await CallAndWriteResponseAsync(() => client.Payload.GetAsync(payloadResult.Uuid));
        }

        private static async Task<XummPayloadResponse> GetPayloadAsync(XummClient client, string txJson)
        {
            var payload = new XummPayload(txJson, default)
            {
                CustomMeta = new XummPayloadCustomMeta
                {
                    Instruction = "Test payload created with the XUMM.Net Wrapper."
                }
            };

            return await CallAndWriteResponseAsync(() => client.Payload.SubmitAsync(payload));
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
