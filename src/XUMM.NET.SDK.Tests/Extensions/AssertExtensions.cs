using System.Text.Json;
using NUnit.Framework;

namespace XUMM.NET.SDK.Tests.Extensions;

internal static class AssertExtensions
{
    internal static void AreEqual(object expected, object actual)
    {
        var expectedJson = JsonSerializer.Serialize(expected);
        var actualJson = JsonSerializer.Serialize(actual);
        Assert.AreEqual(expectedJson, actualJson);
    }
}
