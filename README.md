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