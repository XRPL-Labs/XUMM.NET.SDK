namespace XUMM.NET.SDK.Models.Misc;

public class XummRailsResponse
{
    internal XummRailsResponse(string networkKey, XummRailsNetwork network)
    {
        NetworkKey = networkKey;
        Network = network;
    }

    public string NetworkKey { get; set; }
    public XummRailsNetwork Network { get; set; }
}
