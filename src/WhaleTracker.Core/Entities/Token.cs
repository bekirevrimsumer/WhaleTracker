namespace WhaleTracker.Core.Entities
{
    public class Token : BaseEntity
    {
        public string TokenAddress { get; set; } = string.Empty;
        public string TokenName { get; set; } = string.Empty;
        public string TokenSymbol { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public decimal? Price { get; set; }
        public string? TokenIcon { get; set; }
        public Guid WalletId { get; set; }
        public virtual Wallet Wallet { get; set; } = null!;
    }
} 