namespace XUMM.Net.Configs;

public class ApiConfig
{
    internal static readonly string DefaultRestClientAddress = "https://xumm.app/api/v1";
    internal static readonly string DefaultSectionKey = "Xumm";

    public string RestClientAddress { get; set; } = DefaultRestClientAddress;
    public string ApiKey { get; set; } = default!;
    public string ApiSecret { get; set; } = default!;
}
