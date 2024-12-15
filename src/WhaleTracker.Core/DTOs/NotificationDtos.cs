namespace WhaleTracker.Core.DTOs
{
    public class TokenMovementNotificationDto
    {
        public string WalletAddress { get; set; } = string.Empty;
        public string TokenSymbol { get; set; } = string.Empty;
        public string TokenName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionHash { get; set; } = string.Empty;
    }

    public class TokenNotificationDto
    {
        public string WalletAddress { get; set; } = string.Empty;
        public string TokenSymbol { get; set; } = string.Empty;
        public string TokenName { get; set; } = string.Empty;
        public decimal Balance { get; set; }
    }
} 