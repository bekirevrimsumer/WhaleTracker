namespace WhaleTracker.Core.Entities
{
    public class Wallet : BaseEntity
    {
        public string Address { get; set; } = string.Empty;
        public string? Name { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<Token> Tokens { get; set; } = new List<Token>();
    }
} 