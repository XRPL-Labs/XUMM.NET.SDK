using System.Collections.Generic;
using System.Text.Json;
using XUMM.Net.Models.Misc;

namespace XUMM.Net.Tests.Fixtures;

internal static class MiscFixtures
{
    internal static XummPong XummPong => new()
    {
        Pong = true,
        Auth = new XummAuth
        {
            Quota = new Dictionary<string, object>(),
            Application = new XummApplication
            {
                Uuidv4 = "00000000-0000-4839-af2f-f794874a80b0",
                Name = "SomeApplication",
                WebhookUrl = "https://webhook.site/00000000-0000-4e34-8112-c4391247a8ee",
                Disabled = 0
            },
            Call = new XummCall
            {
                Uuidv4 = "2904b05f-5b37-4f3e-a624-940ad817943c"
            }
        }
    };

    internal static XummCuratedAssets XummCuratedAssets => new()
    {
        Issuers = new List<string>
        {
            "Bitstamp",
            "Wietse"
        },
        Currencies = new List<string>
        {
            "USD",
            "BTC",
            "ETH",
            "WIE"
        },
        Details = new Dictionary<string, XummCuratedAssetsDetails>
        {
            {
                "Bitstamp", new XummCuratedAssetsDetails
                {
                    Id = 185,
                    Name = "Bitstamp",
                    Domain = "bitstamp.net",
                    Avatar = "https://xumm.app/assets/icons/currencies/ex-bitstamp.png",
                    Shortlist = 1,
                    Currencies = new Dictionary<string, XummCuratedAssetsDetailsCurrency>
                    {
                        {
                            "USD", new XummCuratedAssetsDetailsCurrency
                            {
                                Id = 178,
                                IssuerId = 185,
                                Issuer = "rvYAfWj5gh67oV6fW32ZzP3Aw4Eubs59B",
                                Currency = "USD",
                                Name = "US Dollar",
                                Avatar = "https://xumm.app/assets/icons/currencies/fiat-dollar.png",
                                Shortlist = 1
                            }
                        },
                        {
                            "BTC", new XummCuratedAssetsDetailsCurrency
                            {
                                Id = 492,
                                IssuerId = 185,
                                Issuer = "rvYAfWj5gh67oV6fW32ZzP3Aw4Eubs59B",
                                Currency = "BTC",
                                Name = "Bitcoin",
                                Avatar = "https://xumm.app/assets/icons/currencies/crypto-btc.png",
                                Shortlist = 1
                            }
                        }
                    }
                }
            },
            {
                "Wietse", new XummCuratedAssetsDetails
                {
                    Id = 17553,
                    Name = "Wietse",
                    Domain = "wietse.com",
                    Avatar = "https://xumm.app/assets/icons/currencies/wietse.jpg",
                    Shortlist = 0,
                    Currencies = new Dictionary<string, XummCuratedAssetsDetailsCurrency>
                    {
                        {
                            "WIE", new XummCuratedAssetsDetailsCurrency
                            {
                                Id = 17552,
                                IssuerId = 17553,
                                Issuer = "rwietsevLFg8XSmG3bEZzFein1g8RBqWDZ",
                                Currency = "WIE",
                                Name = "Wietse",
                                Avatar = "https://xumm.app/assets/icons/currencies/transparent.png",
                                Shortlist = 0
                            }
                        }
                    }
                }
            }
        }
    };

    internal static XummTransaction XummTransaction => new()
    {
        Txid = "A17E4DEAD62BF705D9B73B4EAD2832F1C55C6C5A0067327A45E497FD8D31C0E3",
        BalanceChanges = new Dictionary<string, List<XummTransactionBalanceChanges>>
        {
            {
                "r4bA4uZgXadPMzURqGLCvCmD48FmXJWHCG", new List<XummTransactionBalanceChanges>
                {
                    new()
                    {
                        CounterParty = string.Empty,
                        Currency = "XRP",
                        Value = "-1.000012"
                    }
                }
            },
            {
                "rPdvC6ccq8hCdPKSPJkPmyZ4Mi1oG2FFkT", new List<XummTransactionBalanceChanges>
                {
                    new()
                    {
                        CounterParty = string.Empty,
                        Currency = "XRP",
                        Value = "1"
                    }
                }
            }
        },
        Node = "wss://xrplcluster.com",
        Transaction = JsonDocument.Parse(
            "{\"Account\":\"r4bA4uZgXadPMzURqGLCvCmD48FmXJWHCG\",\"Amount\":\"1000000\",\"Destination\":\"rPdvC6ccq8hCdPKSPJkPmyZ4Mi1oG2FFkT\"," +
            "\"Fee\":\"12\",\"Flags\":2147483648,\"Sequence\":58549314,\"SigningPubKey\":\"0260F06C0590C470E7E7FA9DE3D9E85B1825E19196D8893DD84431F6E9491739AC\"," +
            "\"TransactionType\":\"Payment\",\"meta\":{\"TransactionIndex\":0,\"TransactionResult\":\"tesSUCCESS\",\"delivered_amount\":\"1000000\"},\"validated\":true}")
    };

    internal static XummRates XummRates => new()
    {
        USD = 75.50455,
        XRP = 54.329676,
        Meta = new XummMeta
        {
            Currency = new XummCurrency
            {
                En = "Indian Rupee",
                Code = "INR",
                Symbol = "₹",
                IsoDecimals = 2
            }
        }
    };
}
