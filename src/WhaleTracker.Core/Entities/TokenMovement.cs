namespace WhaleTracker.Core.Entities
{
    public class TokenMovement : BaseEntity
    {
        public Guid WalletId { get; set; }
        public Guid TokenId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionHash { get; set; } = string.Empty;
        
        public virtual Wallet Wallet { get; set; } = null!;
        public virtual Token Token { get; set; } = null!;
    }
} 