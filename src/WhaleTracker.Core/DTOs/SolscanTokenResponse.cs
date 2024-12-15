namespace WhaleTracker.Core.DTOs;

public class SolscanTokenResponse
{
    public bool Success { get; set; }
    public SolscanTokenData Data { get; set; }
}

public class SolscanTokenData
{
    public IEnumerable<SolscanToken> Tokens { get; set; }
    public int Count { get; set; }
}

public class SolscanToken
{
    public string Address { get; set; }
    public long Amount { get; set; }
    public int Decimals { get; set; }
    public string Owner { get; set; }
    public string TokenAddress { get; set; }
    public string Reputation { get; set; }
    public decimal PriceUsdt { get; set; }
    public string TokenName { get; set; }
    public string TokenSymbol { get; set; }
    public string TokenIcon { get; set; }
    public decimal Balance { get; set; }
    public decimal Value { get; set; }
}
