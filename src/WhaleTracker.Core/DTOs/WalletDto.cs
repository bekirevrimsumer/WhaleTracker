namespace WhaleTracker.Core.DTOs
{
    public class WalletDto
    {
        public Guid Id { get; set; }
        public string Address { get; set; } = string.Empty;
        public string? Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<TokenDto> Tokens { get; set; }
    }

    public class CreateWalletDto
    {
        public string Address { get; set; } = string.Empty;
        public string? Name { get; set; }
        public bool IsActive { get; set; } = true;
    }

    public class UpdateWalletDto
    {
        public string? Name { get; set; }
        public bool IsActive { get; set; }
    }
} 