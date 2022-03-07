using System.Text.Json;
using XUMM.NET.SDK.Models.Misc.AppStorage;

namespace XUMM.NET.SDK.Tests.Fixtures
{
    internal static class MiscAppStorageFixtures
    {
        internal static XummStorage XummStorage => new()
        {
            Application = new XummStorageApplication
            {
                Name = "SomeApplication",
                Uuidv4 = "00000000-0000-4839-af2f-f794874a80b0"
            },
            Data = JsonDocument.Parse("{\"some\": \"other_data\"}")
        };

        internal static XummStorageStore XummStorageStore => new()
        {
            Application = new XummStorageApplication
            {
                Name = "SomeApplication",
                Uuidv4 = "00000000-0000-4839-af2f-f794874a80b0"
            },
            Data = JsonDocument.Parse("{\"name\": \"Wietse\"}"),
            Stored = true
        };

        internal static XummStorageStore XummStorageDelete => new()
        {
            Application = new XummStorageApplication
            {
                Name = "SomeApplication",
                Uuidv4 = "00000000-0000-4839-af2f-f794874a80b0"
            },
            Data = null,
            Stored = true
        };
    }
}
