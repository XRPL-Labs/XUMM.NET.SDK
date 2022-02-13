using System.Collections.Generic;
using XUMM.Net.Models.Misc;

namespace XUMM.Net.Tests.Fixtures;

internal static class MiscFixtures
{
    public static XummPong XummPong => new()
    {
        Pong = true,
        Auth = new XummAuth
        {
            Quota = new Dictionary<string, object>(),
            Application = new XummApplication
            {
                Uuidv4 = "00000000-0000-4839-af2f-f794874a80b0",
                Name = "SomeApplication",
                WebhookUrl = "https://webhook.site/00000000-0000-4e34-8112-c4391247a8ee",
                Disabled = 0
            },
            Call = new XummCall
            {
                Uuidv4 = "2904b05f-5b37-4f3e-a624-940ad817943c"
            }
        }
    };
}
