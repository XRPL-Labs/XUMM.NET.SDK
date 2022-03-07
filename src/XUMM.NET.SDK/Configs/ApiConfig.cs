using System;
using XUMM.NET.SDK.Extensions;

namespace XUMM.NET.SDK.Configs;

public class ApiConfig
{
    internal const string DefaultRestClientAddress = "https://xumm.app/api/v1";
    internal const string DefaultSectionKey = "Xumm";

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
