using System.Text.Json.Serialization;
using XUMM.NET.SDK.Enums;

namespace XUMM.NET.SDK.Models.Payload.XRPL;

/// <summary>
/// Create or modify a trust line linking two accounts.
/// </summary>
public class XrplTrustSetTransaction : XrplTransaction
{
    /// <param name="account">The unique address of the account that initiated the transaction.</param>
    /// <param name="currency">
    /// The currency to this trust line applies to, as a three-letter ISO 4217 Currency Code or a
    /// 160-bit hex value according to currency format. "XRP" is invalid.
    /// </param>
    /// <param name="issuer">The address of the account to extend trust to.</param>
    /// <param name="value">Quoted decimal representation of the limit to set on this trust line.</param>
    /// <param name="fee">
    /// Integer amount of XRP, in drops, to be destroyed as a cost for distributing this transaction to the network.
    /// </param>
    public XrplTrustSetTransaction(string account, string currency, string issuer, string value, int fee) : this()
    {
        Account = account;
        LimitAmount = new XrplTrustSetLimitAmount
        {
            Currency = currency,
            Issuer = issuer,
            Value = value
        };
        Fee = fee.ToString();
    }

    public XrplTrustSetTransaction()
    {
        TransactionType = XrplTransactionType.TrustSet.ToString();
    }

    /// <summary>
    /// Transactions of the TrustSet type support additional values in the Flags field.
    /// </summary>
    [JsonPropertyName("Flags")]
    public new XrplTrustSetFlags? Flags { get; set; }

    /// <summary>
    /// Object defining the trust line to create or modify, in the format of a Currency Amount.
    /// </summary>
    [JsonPropertyName("LimitAmount")]
    public XrplTrustSetLimitAmount LimitAmount { get; set; } = default!;
}
