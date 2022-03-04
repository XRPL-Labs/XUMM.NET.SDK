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
