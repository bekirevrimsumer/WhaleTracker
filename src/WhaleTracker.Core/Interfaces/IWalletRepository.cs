using WhaleTracker.Core.Entities;

namespace WhaleTracker.Core.Interfaces
{
    public interface IWalletRepository
    {
        Task<IEnumerable<Wallet>> GetAllWalletsAsync();
        Task<Wallet?> GetWalletByIdAsync(Guid id);
        Task<Wallet?> GetWalletByAddressAsync(string address);
        Task<Wallet> AddWalletAsync(Wallet wallet);
        Task UpdateWalletAsync(Wallet wallet);
        Task DeleteWalletAsync(Guid id);
        Task<Token?> GetTokenByAddressAsync(string tokenAddress, Guid walletId);
        Task<Token> AddTokenAsync(Token token);
        Task UpdateTokenAsync(Token token);
    }
} 