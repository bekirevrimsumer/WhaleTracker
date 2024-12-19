namespace WhaleTracker.Core.DTOs
{
    public class TokenDto
    {
        public string TokenAddress { get; set; }
        public string TokenName { get; set; }
        public string TokenSymbol { get; set; }
        public decimal Balance { get; set; }
        public decimal? Price { get; set; }
        public string? TokenIcon { get; set; }
    }
} 