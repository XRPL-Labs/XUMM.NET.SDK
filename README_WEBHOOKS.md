# XUMM.NET.SDK [![XUMM.NET.SDK](https://github.com/XRPL-Labs/XUMM.NET.SDK/actions/workflows/dotnet.yml/badge.svg)](https://github.com/XRPL-Labs/XUMM.NET.SDK/actions/workflows/dotnet.yml)
Receive webhooks of XUMM in .NET / C# environments.

## Install XUMM.NET.SDK.Webhooks in server-side Blazor App

1. Create a new ASP.NET Core Blazor Server-App project.
2. Install NuGet-package: `XUMM.NET.SDK.Webhooks`.
3. Create an implementation of `IXummWebhookProcessor`, for example `XummWebhookProcessor`.
3. Add the following code block before `builder.Build()` in `Program.cs`:

```c#
builder.Services.AddXummWebhooks<XummWebhookProcessor>();
```

4. Add the following code block before `app.Run()` in `Program.cs` to map the Xumm Webhooks controller.:
 
```c#
app.MapXummControllerRoute();
```

5. Hit `F5`: you're now running a completely empty Blazor server-side App with XUMM.NET.SDK.Webhooks. 
6. Start building your app. For reference, browse the [XUMM.NET.ServerApp](https://github.com/XRPL-Labs/XUMM.NET.SDK/tree/main/XUMM.NET.ServerApp) to see all the options.
