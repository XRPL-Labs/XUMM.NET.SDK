﻿using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Models.Misc;

public class XummCuratedAssetsDetails
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("domain")]
    public string? Domain { get; set; }

    [JsonPropertyName("avatar")]
    public string? Avatar { get; set; }

    [JsonPropertyName("shortlist")]
    public int Shortlist { get; set; }

    [JsonPropertyName("currencies")]
    public Dictionary<string, XummCuratedAssetsDetailsCurrency> Currencies { get; set; } = default!;
}