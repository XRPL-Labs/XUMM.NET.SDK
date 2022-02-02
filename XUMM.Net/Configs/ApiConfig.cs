using System;
using XUMM.Net.Extensions;

namespace XUMM.Net.Configs;

public class ApiConfig
{
    internal static readonly string DefaultRestClientAddress = "https://xumm.app/api/v1";
    internal static readonly string DefaultSectionKey = "Xumm";

    private string _apiKey = default!;
    private string _apiSecret = default!;

    public string RestClientAddress { get; set; } = DefaultRestClientAddress;

    public string ApiKey
    {
        get => _apiKey;
        set
        {
            if (!value.IsValidUuid())
            {
                throw new Exception("Invalid API Key.");
            }

            _apiKey = value;
        }
    }

    public string ApiSecret
    {
        get => _apiSecret;
        set
        {
            if (!value.IsValidUuid())
            {
                throw new Exception("Invalid API Secret.");
            }

            _apiSecret = value;
        }
    }
}
