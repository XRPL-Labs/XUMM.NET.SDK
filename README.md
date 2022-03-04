# XUMM.NET [![XUMM.NET](https://github.com/DominiqueBlomsma/XUMM.Net/actions/workflows/dotnet.yml/badge.svg?branch=main)](https://github.com/DominiqueBlomsma/XUMM.Net/actions/workflows/dotnet.yml)
Interact with the XUMM SDK from .NET / C# environments.

## Install XUMM.NET in server-side Blazor App

1. Create a new ASP.NET Core Blazor Server-App project.
2. Install NuGet-package: `XUMM.NET`.
3. Add the following code block at the end of the services in `Program.cs`:

```c#
builder.Services.AddXummNet(builder.Configuration);
```

4. Add the following configuration in `appsettings.json`:

```json
  "Xumm": {
    "RestClientAddress": "https://xumm.app/api/v1",
    "ApiKey": "", // API Key which can be obtained from the xumm Developer Console.
    "ApiSecret": "" // API Secret which can be obtained from the xumm Developer Console.
  },
````

5. Hit `F5`: you're now running a completely empty Blazor server-side App with XUMM.NET. 
6. Start building your app. For reference, browse the [XUMM.Net.ServerApp](https://github.com/DominiqueBlomsma/XUMM.Net/tree/main/XUMM.Net.ServerApp) to see all the options.

### Credentials

The SDK will look in your appsettings for the `ApiKey` and `ApiSecret` values. Optionally the `RestClientAddress` can be provided. An [example appsettings](https://github.com/DominiqueBlomsma/XUMM.Net/blob/main/XUMM.Net.ServerApp/appsettings.json) file is provided in this repository. Alternatively you can provide your XUMM API Key & Secret by passing them like:

```C#
builder.Services.AddXummNet(o =>
    {
        o.ApiKey = "00000000-0000-0000-000-000000000000";
        o.ApiSecret = "00000000-0000-0000-000-000000000000";
    });
```

Create your app and get your XUMM API credentials at the XUMM Developer Console:

- https://apps.xumm.dev

More information about the XUMM API, payloads, the API workflow, sending Push notifications, etc. please check the XUMM API Docs: 

- https://xumm.readme.io/docs


##### IXummMiscClient.GetPingAsync()

The `ping` method allows you to verify API access (valid credentials) and returns some info on your XUMM APP:

```C#
@inject IXummMiscClient _miscClient
var pong = await _miscClient.GetPingAsync();
```

Returns [`XummPong`](https://github.com/DominiqueBlomsma/XUMM.Net/blob/main/XUMM.Net/Models/Misc/XummPong.cs):
```C#
var pong = new XummPong
{
    Pong = true,
    Auth = new XummAuth
    {
        Quota = new Dictionary<string, object>
        {
            { "ratelimit", null}
        },
        Application = new XummApplication
        {
            Uuidv4 = "00000000-1111-2222-3333-aaaaaaaaaaaa",
            Name = "My XUMM APP",
            WebhookUrl = "",
            Disabled = 0
        },
        Call = new XummCall
        {
            Uuidv4 = "bbbbbbbb-cccc-dddd-eeee-111111111111"
        }
    }
}
```


##### IXummMiscClient.GetKycStatusAsync()

The `GetKycStatusAsync` return the KYC status of a user based on a user_token, issued after the
user signed a Sign Request (from your app) before (see Payloads - Intro).

If a user token specified is invalid, revoked, expired, etc. the method will always
return `XummKycStatus.None`, just like when a user didn't go through KYC. You cannot distinct a non-KYC'd user
from an invalid token.

Alternatively, KYC status can be retrieved for an XPRL account address: the address selected in
XUMM when the session KYC was initiated by.

```C#
@inject IXummMiscClient _miscClient
var kycStatus = await _miscClient.GetKycStatusAsync("00000000-0000-0000-0000-000000000000");
```

Returns [`XummKycStatus`](https://github.com/DominiqueBlomsma/XUMM.Net/blob/main/XUMM.Net/Enums/XummKycStatus.cs).

###### Notes on KYC information

- Once an account has successfully completed the XUMM KYC flow, the KYC flag will be applied to the account even if the identity document used to KYC expired. The flag shows that the account was **once** KYC'd by a real person with a real identity document.
- Please note that the KYC flag provided by XUMM can't be seen as a "all good, let's go ahead" flag: it should be used as **one of the data points** to determine if an account can be trusted. There are situations where the KYC flag is still `true`, but an account can no longer be trusted. Eg. when account keys are compromised and the account is now controlled by a 3rd party. While unlikely, depending on the level of trust required for your application you may want to mitigate against these kinds of fraud.


##### IXummMiscClient.GetTransactionAsync()

The `GetTransactionAsync` method allows you to get the transaction outcome (mainnet)
live from the XRP ledger, as fetched for you by the XUMM backend.

**Note**: it's best to retrieve these results **yourself** instead of relying on the XUMM platform to get live XRPL transaction information! You can use the **[xrpl-txdata](https://www.npmjs.com/package/xrpl-txdata)** package to do this:  
[![npm version](https://badge.fury.io/js/xrpl-txdata.svg)](https://www.npmjs.com/xrpl-txdata)

```C#
@inject IXummMiscClient _miscClient
var txInfo = await _miscClient.GetTransactionAsync("00000000-0000-0000-0000-000000000000");
```

Returns: `<XummTransaction>`](https://github.com/DominiqueBlomsma/XUMM.Net/blob/main/XUMM.Net/Models/Misc/XummTransaction.cs)