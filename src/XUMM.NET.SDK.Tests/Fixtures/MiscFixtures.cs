using System;
using System.Collections.Generic;
using System.Text.Json;
using XUMM.NET.SDK.Models.Misc;

namespace XUMM.NET.SDK.Tests.Fixtures;

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
                RedirectUris = new List<string>
                {
                    "https://redirect.site/00000000-0000-4e34-8112-c4391247a8ee",
                    "https://redirect.site/00000000-4e34-0000-8112-c4391247a8ee"
                },
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

    internal static string XummRailsNetworkKey => "MAINNET";

    internal static XummRailsNetwork XummRailsNetwork => new()
    {
        ChainId = 0,
        Color = "#3C06F3",
        Name = "XRP Ledger Mainnet",
        IsLivenet = true,
        NativeAsset = "XRP",
        Endpoints = new List<XummRailsNetworkEndpoint>
                {
                    new()
                    {
                        Name = "XRPLCluster.com (XRPLF)",
                        Url = "wss://xrplcluster.com"
                    }
                },
        Explorers = new List<XummRailsNetworkExplorer>
                {
                    new()
                    {
                        Name = "XRPL.org Livenet Explorer",
                        UrlTx = "https://livenet.xrpl.org/transactions/",
                        UrlAccount = "https://livenet.xrpl.org/accounts/"
                    }
                },
        Rpc = "https://xrplcluster.com",
        Definitions = "default",
        Icons = new XummRailsNetworkIcons
        {
            IconSquare = "data:image/png;base64,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",
            IconAsset = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAMAAAADACAQAAAD41aSMAAAJ4ElEQVR42u2de3CVxRnGfyeBBFBuhWgVUGoVEBUIEFoYbaHDZFpRp6RoCdKCoGkhzDDodEZuEnAU7EgQxGqGcpM6bVGQix0oFIu2A3JxCh1NGxksIBSQaYVAhQSS0z/STLCSkJzzPd+3u2ef/X/33ed8Zy/Pvhfw8PDw8PDw8PDwiAydyOXBJrfhDOVOMlKWtUx6MZS8BJjLpVNdN1mspYp4wu0iO5jJzSlF/S0UsYtLSbBWxRqyAFrzURLdXN7hb+iREuTfwRtJfbB1rYzWMDeQrmpaBU+R7jT5zXiGygAZexYOBNhdnDhbaecs/R14J2C2yuBswF3G2ctXnKQ/i/2Bc1UOZYF3Guc9rnWO/ra8L2CqDOYIuo2zlUyn6G8Z+OJT0+ZAK0olXa9xaDtuzkYJR6W0AujCYUn3K4k5QX+MZRJ+jtK1dohunJQMsciJH6BYws0pel4+SA7lkmGmWk+/Zo8sp///DzSE85KhJltNf6GEkwpyrzTYA1wUDFbFSGvpHx2Q5PDFdokR4Q5Yyb1W0q/5IKsZ39CgkyR/uc+5xzr6B4uW5Cei2XROk20V/b35TMJDUXTHrk/pbg39t3FCwsFL0V48jnCTFfR35pBk/q+R1lgT0lkjMeEDOligef5NMveNNG+a+LRdYsYuWhtNfxv2Sua9g2tMMWUbLVJO89xP+0TM6SjSSdfRzFDN8y3JfA9wvWnb0asG6qSqo8cndZqnSQcy83RSleZ5u6lXkmkpoHmeoV8Qxg3inOM6aaFIgPl2UAbeH6gvjGk6qUrzzAvSyIed1UlVmucjQRs60UmdVKV5TlEYO8s5nVR1wJilMvh5p3RS1RF7sfKy8ktndFLVJXNV4zXPRJDO6xKzy7jOCc1zg15myeD3EtN3h6iTqoTGP4YjNLZmj9U6qUrz3BeeY35HPrRWJ1Vpnh8lrnmatIUtFeukafxKYvdhuoR9ilD5ky4W/gQxfiGx+STdojhHZ3NaMp0Fop8gTXSIjvAqOVCkk/6WloID9AqRmPKtKLWUYRKdNM67NTG0AZ77t0jsrGRY1GpiXlLhyg2tq8FNbYDoyFDNWBP0dI1OGqeaF2kbwKm/SCI4x4lTaMqL0nTRBOOcYFxS2soI0bcfJ84Mk95U58umGaeUMU3zKvvfmec+0Y23phUb9aYtc+qo00vncGujrWnPpIAyYNTXlpvnWJPOWumU41TzHkUMbCD8NY2e/IztsjW/tr1ppmtZJn8QT7z25L2HJUxlAvl8j/vIp4BpLGW36FZikXNlK/4cCgVRtt1muxe3Y5/T9If9cJQAbuRjZ+m3JMTk6/zTSfotCrK6i385R/9p+mIRviFIBBVlszDQdigXnKHf0lDz4SKdNOxmcbKFMVQ78ANYnW7kSevptz7hznNW078Q6xHjZWvpL3Ej6ZqtP8EyrZttmEhjuac/6n/Bz62if5Fb9NdgqiWH0moHUg3Wgwf5j/H0n+dHOIwcjhlN/9Evp5N0DVlsMpb+t7mBFEAaM+TP5k1vF5nh4sZbH3ob9nT5IQNIMbTgOUP+B5XMMziJlBTdRT7LTWnvcBcpjBj5HIyM/IOMdCTFflLIoJDjoZN/nMIEPE6dRSY/llSzuXL7B5Nrald4XI5mjBQly/zimj/S0KSBhuB2ikV+RccoTj6DW2ogne9QEqBocYwShqTSNSuoE1I209mahIfRWbYyjWx/0kl2b8hhAq+wkzON9GHbyStMIMev9cGjAznkMZGZvEAJK1nNalZSwgvMZALD6W9BenAPDw8PDw8PDw8PDw8PDw8PDw8PDw8Pj6shnc70IZdRFFDAFKZQQAGjyKUPnRtI5uSRBJrTl0dYwBYOUNHga1gFB9jCAsbS13v9JI8MhvI02/k8wfwO23maoWR4IpuO9oxjbUAJP86ylnGJ1TNNza/+IdYLEn1cYB0P+X9Dw+jKXFFS/LqEyHP5mif6SujNqyHFC1SxMfXCMRpGfzaH7h29yf1wvMahO69HFD9czepoKl+Yg1YURZxXq5KFZucBVSKPo0bEiH3C91OP/HaUGBUluZqOqUR/bgQhSVcPWcpNFTGtSFIcOohNeaH7+lGWtKhC8m17wMWDDMOdFuSVPhJhgWn5yl9uRb6gcjd3g+Gi6u2KVmFvqtb6MMHQjbd+veinLtE/zsIMutVMdIX+MZZ9/XU/wU/cWPvtTeFdZf9eYHsSe0sT19fChTIOFpZuqIUrhUwsK15SC5dK+VhUvqcWrhWzsqSAVZ3a7145NwtKuNU9NLpZ0HC3HY+XGRF4OISXWdf49JbprHaW/jhx1pudAifGEqfpjxNnlcmZt54PLc9zKW+xikXM5lleZAVvUhpaHt5iU+mfHsKtdDOP07+elTiTPoxnTQi37xlmKv5aWWwLoxuZ6zOT7/JrKqX2GCdV5wsl5wu8zC1NtqgTz3BK+EGMMon+e2XfWxVLuDFhu9oyX2ZZJcNMof9uWWWYnfRL2roesgzthuikvfi36KQzLyA3qRiTrxLal2g7E8AHkiRu44QozfDAQO38JodFETeROrh35pBkWvvoHLitWfxFYushga2NREdKRYrLtRJ727NLYm9pNJ7Vrdkjmc6fuEZmcxuRTruHNmHT34K3JVPZJZZ82/K+CzppOm9IpvFBCJmfVQvnhvB00hhLrX72Ux0dQtNJ54sOdOE9fKsOz4vDMH6WyPUjXK/83nwmmccsteETnbnUDxa5yz+uNPphieYZlfvf/ZInnGrG6QxWKItROsCOlnxQl/iBwthBnJP8ZSdHqmUVSuZ0nsG2bFrRV2+fLdJJA00Fojq2LTTiPaNYMrdTwRWNU11cVhpS3yvGMlEOiq4mX93XGeTilM4ayRwPcH3y+uFeiWnbDHPyaykqI7o/uWSBKrN2iRR/Ez+1HYnL683ZaK3madJiuzGxt23V1mRyqIPquPFaIjqp5nB20vBgH9WB+6WmGjLHCc3TpCvn7KYYMSmlAz5VOukT0UpUNoU8q3TS8Y0Z/AHJ4LYF/at00hFXG3iI5O9nY9oLjU5a0XAyqBxRTqsnsREanfQcg+obsJsoc/lCbIVKJ+15pcG6iNxXTdE8TbqMHv2yTprF353XPM3SSb96+TAqh71t5gc2RyZI/rVOJ23JuymjeZqkk+6s0UljbEgpzTMRXCdaoNcTg8ckXX+cRGidibiJIxKeHkUStHCcW3ENPfhUskxzRnDO7YWL6CcIRzxD4InFjnAHriI7cLaOwYpAO9zkdvp3bmBboHwth66BJbc4SD7uI8bYwB4uy7kZYEDSO/x5fscPU6h6aQaj2Jx0Stoj5NR1mMdTzEugTedR7nbgvpvYHfkeHmNGQrzNIs/XtfTw8PDw8PDw8PBIVfwX0X8tlQ9X7JkAAAAASUVORK5CYII="
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

    internal static XummUserTokens XummUserToken => new()
    {
        Tokens = new List<XummUserTokenValidity>
        {
            new()
            {
                UserToken = "691d5ae8-968b-44c8-8835-f25da1214f35",
                Active = true
            }
        }
    };

    internal static XummUserTokens XummUserTokens => new()
    {
        Tokens = new List<XummUserTokenValidity>
        {
            new()
            {
                UserToken = "691d5ae8-968b-44c8-8835-f25da1214f35",
                Active = true
            },
            new()
            {
                UserToken = "b12b59a8-83c8-4bc0-8acb-1d1d743871f1",
                Active = false
            }
        }
    };

    internal static XummAccountMetaResponse XummAccountMeta => new()
    {
        Account = "rwietsevLFg8XSmG3bEZzFein1g8RBqWDZ",
        KycApproved = true,
        XummPro = true,
        Avatar = "https://xumm.app/avatar/rwietsevLFg8XSmG3bEZzFein1g8RBqWDZ.png",
        XummProfile = new()
        {
            AccountAlias = "XRPL Labs - Wietse Wind",
            OwnerAlias = "Wietse Wind"
        },
        ThirdPartyProfiles = new()
        {
            new XummThirdPartyProfile()
            {
                AccountAlias="Wietse Wind",
                Source= "xumm.app"
            },
            new XummThirdPartyProfile()
            {
                AccountAlias= "wietse.com",
                Source= "xrpl"
            }
        },
        GlobaliD = new()
        {
            Linked = new DateTime(2021, 06, 29, 10, 22, 25, 123, DateTimeKind.Utc),
            ProfileUrl = "https://global.id/wietse"
        }
    };
}
