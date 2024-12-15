using WhaleTracker.Core.DTOs;

namespace WhaleTracker.Core.Interfaces
{
    public interface ISolscanService
    {
        Task<IEnumerable<TokenDto>> GetWalletTokensAsync(string walletAddress);
    }
} 