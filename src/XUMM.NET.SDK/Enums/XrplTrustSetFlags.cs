using System;

namespace XUMM.NET.SDK.Enums;

[Flags]
public enum XrplTrustSetFlags
{
    /// <summary>
    /// Authorize the other party to hold currency issued by this account. (No effect unless using the asfRequireAuth
    /// AccountSet flag.) Cannot be unset.
    /// </summary>
    tfSetfAuth = 65536,

    /// <summary>
    /// Enable the No Ripple flag, which blocks rippling between two trust lines of the same currency if this flag is enabled
    /// on both.
    /// </summary>
    tfSetNoRipple = 131072,

    /// <summary>
    /// Disable the No Ripple flag, allowing rippling on this trust line.)
    /// </summary>
    tfClearNoRipple = 262144,

    /// <summary>
    /// Freeze the trust line.
    /// </summary>
    tfSetFreeze = 1048576,

    /// <summary>
    /// Unfreeze the trust line.
    /// </summary>
    tfClearFreeze = 2097152
}
