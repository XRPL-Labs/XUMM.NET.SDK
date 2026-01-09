using System.Text.Json;
using XUMM.NET.SDK.Models.XApp;

namespace XUMM.NET.SDK.Extensions;

/// <summary>
/// Extension methods for XummXAppOriginDataResponse to safely access additional data
/// </summary>
public static class XummXAppOriginDataExtensions
{
    /// <summary>
    /// Tries to get a JsonElement from the additional data
    /// </summary>
    public static bool TryGetAdditionalData(this XummXAppOriginDataResponse? response, string key, out JsonElement? jsonElement)
    {
        jsonElement = null;
        if (response?.AdditionalData?.TryGetValue(key, out var element) == true)
        {
            jsonElement = element;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Tries to get a string value from the additional data
    /// </summary>
    public static bool TryGetAdditionalDataAsString(this XummXAppOriginDataResponse? response, string key, out string? value)
    {
        value = null;

        if (TryGetAdditionalData(response, key, out var element) && element.HasValue)
        {
            value = element.Value.ValueKind switch
            {
                JsonValueKind.String => element.Value.GetString(),
                JsonValueKind.Number => element.Value.ToString(),
                JsonValueKind.Null => null,
                _ => element.Value.ToString()
            };
            return true;
        }

        return false;
    }

    /// <summary>
    /// Tries to get an numberic value from the additional data (handles both int32 and int64)
    /// </summary>
    public static bool TryGetAdditionalDataAsNumber(this XummXAppOriginDataResponse? response, string key, out long value)
    {
        value = 0;
        if (TryGetAdditionalData(response, key, out var element) && element.HasValue)
        {
            var result = element.Value.ValueKind switch
            {
                JsonValueKind.Number when element.Value.TryGetInt32(out var int32) => int32,
                JsonValueKind.Number => element.Value.GetInt64(),
                JsonValueKind.String when long.TryParse(element.Value.GetString(), out var parsedLong) => parsedLong,
                _ => (long?)null
            };

            if (result.HasValue)
            {
                value = result.Value;
                return true;
            }
        }

        return false;
    }
}
