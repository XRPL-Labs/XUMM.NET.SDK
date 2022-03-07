﻿using System.Text.Json.Serialization;

namespace XUMM.Net.Models.Misc
{
    public class XummTransactionBalanceChangesFormatted
    {
        [JsonPropertyName("value")]
        public string Value { get; set; } = default!;

        [JsonPropertyName("currency")]
        public string Currency { get; set; } = default!;
    }
}