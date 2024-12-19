using System.Text.Json.Serialization;

namespace WhaleTracker.Core.DTOs;

public class SolscanTokenResponse
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }
    [JsonPropertyName("data")]
    public SolscanTokenData Data { get; set; }
}

public class SolscanTokenData
{
    [JsonPropertyName("tokens")]
    public IEnumerable<SolscanToken> Tokens { get; set; }
    [JsonPropertyName("count")]
    public int Count { get; set; }
}

public class SolscanToken
{
    [JsonPropertyName("address")]
    public string Address { get; set; }
    [JsonPropertyName("amount")]
    public long Amount { get; set; }
    [JsonPropertyName("decimals")]
    public int Decimals { get; set; }
    [JsonPropertyName("owner")]
    public string Owner { get; set; }
    [JsonPropertyName("tokenAddress")]
    public string TokenAddress { get; set; }
    [JsonPropertyName("reputation")]
    public string Reputation { get; set; }
    [JsonPropertyName("priceUsdt")]
    public double? PriceUsdt { get; set; }
    [JsonPropertyName("tokenName")]
    public string TokenName { get; set; }
    [JsonPropertyName("tokenSymbol")]
    public string TokenSymbol { get; set; }
    [JsonPropertyName("tokenIcon")]
    public string TokenIcon { get; set; }
    [JsonPropertyName("balance")]
    public decimal Balance { get; set; }
    [JsonPropertyName("value")]
    public decimal Value { get; set; }
}
