using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Models.XApp;

public class XummXAppOriginDataResponse
{
    [JsonPropertyName("payload")]
    public string? Payload { get; set; }

    /// <summary>
    /// Additional fields from the API response that aren't part of the standard model.
    /// For easy access, use extension methods like <see cref="Extensions.XummXAppOriginDataExtensions.TryGetAdditionalDataAsString"/> 
    /// or <see cref="Extensions.XummXAppOriginDataExtensions.TryGetAdditionalDataAsNumber"/>.
    /// For advanced scenarios, you can retrieve the raw JsonElement using <see cref="Extensions.XummXAppOriginDataExtensions.TryGetAdditionalData"/> 
    /// and work with it directly.
    /// </summary>
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? AdditionalData { get; set; }
}