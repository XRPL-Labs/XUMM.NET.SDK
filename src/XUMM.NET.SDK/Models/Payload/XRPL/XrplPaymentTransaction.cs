﻿using System.Globalization;
using System.Text.Json.Serialization;
using XUMM.NET.SDK.Enums;
using XUMM.NET.SDK.Extensions;

namespace XUMM.NET.SDK.Models.Payload.XRPL;

public class XrplPaymentTransaction : XrplTransaction
{
    /// <summary>
    /// A Payment transaction represents a transfer of value from one account to another. (Depending on the path taken, this can involve additional exchanges of value, which occur atomically.)
    /// </summary>
    /// <param name="destination">The unique address of the account receiving the payment.</param>
    /// <param name="destinationTag">(Optional) Arbitrary tag that identifies the reason for the payment to the destination, or a hosted recipient to pay.</param>
    /// <param name="fee">Integer amount of XRP, in drops, to be destroyed as a cost for distributing this transaction to the network.</param>
    public XrplPaymentTransaction(string destination, uint? destinationTag, int fee) : this()
    {
        Destination = destination;
        DestinationTag = destinationTag;
        Fee = fee.ToString();
    }

    public XrplPaymentTransaction()
    {
        TransactionType = XrplTransactionType.Payment.ToString();
    }

    /// <summary>
    /// Transactions of the Payment type support additional values in the Flags field.
    /// </summary>
    [JsonPropertyName("Flags")]
    public new XrplPaymentFlags? Flags { get; set; }

    /// <summary>
    /// The unique address of the account receiving the payment.
    /// </summary>
    public string Destination { get; set; } = default!;

    /// <summary>
    /// (Optional) Arbitrary tag that identifies the reason for the payment to the destination, or a hosted recipient to pay.
    /// </summary>
    public uint? DestinationTag { get; set; }

    /// <summary>
    /// (Optional) Arbitrary 256-bit hash representing a specific reason or identifier for this payment.
    /// </summary>
    [JsonPropertyName("InvoiceID")]
    public string? InvoiceId { get; set; }

    /// <summary>
    /// (Optional, auto-fillable) Array of payment paths to be used for this transaction. Must be omitted for XRP-to-XRP transactions.
    /// </summary>
    [JsonPropertyName("Paths")]
    public XrplPaymentPathSpecification[][]? Paths { get; set; }

    /// <summary>
    /// The amount of currency to deliver. For non-XRP amounts, the nested field names MUST be lower-case. If the
    /// tfPartialPayment flag is set, deliver up to this amount instead.
    /// </summary>
    [JsonPropertyName("Amount")]
    public object? Amount { get; private set; }

    /// <summary>
    /// (Optional) Highest amount of source currency this transaction is allowed to cost, including transfer fees, exchange
    /// rates, and slippage.
    /// </summary>
    [JsonPropertyName("SendMax")]
    public object? SendMax { get; private set; }

    /// <summary>
    /// (Optional) Minimum amount of destination currency this transaction should deliver. Only valid if this is a partial
    /// payment.
    /// </summary>
    [JsonPropertyName("DeliverMin")]
    public object? DeliverMin { get;  private set; }

    /// <summary>
    /// XRP Amount
    /// </summary>
    public void SetAmount(decimal amount)
    {
        Amount = amount.XrpToDropsString();
    }

    /// <summary>
    /// XRP Amount
    /// </summary>
    public void SetSendMaxAmount(decimal amount)
    {
        SendMax = amount.XrpToDropsString();
    }

    /// <summary>
    /// XRP Amount
    /// </summary>
    public void SetDeliverMinAmount(decimal amount)
    {
        DeliverMin = amount.XrpToDropsString();
    }

    /// <summary>
    /// Non-XRP Amounts
    /// </summary>
    public void SetAmount(string currency, decimal amount, string issuer)
    {
        Amount = new XrplTransactionCurrencyAmount
        {
            Currency = currency,
            Value = amount.ToString(CultureInfo.InvariantCulture),
            Issuer = issuer
        };
    }

    /// <summary>
    /// Non-XRP Amounts
    /// </summary>
    public void SetSendMaxAmount(string currency, decimal amount, string issuer)
    {
        SendMax = new XrplTransactionCurrencyAmount
        {
            Currency = currency,
            Value = amount.ToString(CultureInfo.InvariantCulture),
            Issuer = issuer
        };
    }

    /// <summary>
    /// Non-XRP Amounts
    /// </summary>
    public void SetDeliverMinAmount(string currency, decimal amount, string issuer)
    {
        DeliverMin = new XrplTransactionCurrencyAmount
        {
            Currency = currency,
            Value = amount.ToString(CultureInfo.InvariantCulture),
            Issuer = issuer
        };
    }
}
