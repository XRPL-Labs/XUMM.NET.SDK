namespace XUMM.NET.SDK.Models.Misc;

public class XummHookInfoResponse
{
    internal XummHookInfoResponse(string hookHash, XummHookInfo hookInfo)
    {
        HookHash = hookHash;
        HookInfo = hookInfo;
    }

    public string HookHash { get; } 
    public XummHookInfo HookInfo { get;  }
}
