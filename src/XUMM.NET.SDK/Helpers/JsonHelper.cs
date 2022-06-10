﻿using System.Text.Json;
using System.Text.Json.Serialization;

namespace XUMM.NET.SDK.Helpers
{
    internal class JsonHelper
    {
        internal static JsonSerializerOptions SerializerOptions => new JsonSerializerOptions
        {
#if NET5_0_OR_GREATER
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
#else
            IgnoreNullValues = true,
#endif
            Converters =
            {
                new JsonStringEnumConverter()
            }
        };
    }
}
