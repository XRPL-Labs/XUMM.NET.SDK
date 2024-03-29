﻿using System.Text.Json;
using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Models.Misc.AppStorage;

public class XummStorage
{
    [JsonPropertyName("application")]
    public XummStorageApplication Application { get; set; } = default!;

    [JsonPropertyName("data")]
    public JsonDocument? Data { get; set; }
}