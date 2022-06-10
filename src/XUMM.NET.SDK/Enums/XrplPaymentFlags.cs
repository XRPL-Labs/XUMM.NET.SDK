using System;

namespace XUMM.NET.SDK.Enums;

[Flags]
public enum XrplPaymentFlags
{
    /// <summary>
    /// Do not use the default path; only use paths included in the Paths field. This is intended to force the transaction to take arbitrage opportunities. Most clients do not need this.
    /// </summary>
    tfNoDirectRipple = 65536,

    /// <summary>
    /// If the specified Amount cannot be sent without spending more than SendMax, reduce the received amount instead of failing outright.
    /// </summary>
    tfPartialPayment = 131072,

    /// <summary>
    /// Only take paths where all the conversions have an input:output ratio that is equal or better than the ratio of Amount:SendMax.
    /// </summary>
    tfLimitQuality = 262144
}
