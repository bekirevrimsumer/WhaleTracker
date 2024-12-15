using Microsoft.EntityFrameworkCore;
using WhaleTracker.Core.Entities;
using WhaleTracker.Core.Interfaces;
using WhaleTracker.Infrastructure.Data;

namespace WhaleTracker.Infrastructure.Repositories
{
    public class WalletRepository : IWalletRepository
    {
        private readonly ApplicationDbContext _context;

        public WalletRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Wallet>> GetAllWalletsAsync()
        {
            return await _context.Wallets
                .Include(w => w.Tokens)
                .ToListAsync();
        }

        public async Task<Wallet?> GetWalletByIdAsync(Guid id)
        {
            return await _context.Wallets
                .Include(w => w.Tokens)
                .FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<Wallet?> GetWalletByAddressAsync(string address)
        {
            return await _context.Wallets
                .Include(w => w.Tokens)
                .FirstOrDefaultAsync(w => w.Address == address);
        }

        public async Task<Wallet> AddWalletAsync(Wallet wallet)
        {
            wallet.CreatedAt = DateTime.UtcNow;
            _context.Wallets.Add(wallet);
            await _context.SaveChangesAsync();
            return wallet;
        }

        public async Task UpdateWalletAsync(Wallet wallet)
        {
            wallet.UpdatedAt = DateTime.UtcNow;
            _context.Entry(wallet).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteWalletAsync(Guid id)
        {
            var wallet = await _context.Wallets.FindAsync(id);
            if (wallet != null)
            {
                _context.Wallets.Remove(wallet);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Token?> GetTokenByAddressAsync(string tokenAddress, Guid walletId)
        {
            return await _context.Tokens
                .FirstOrDefaultAsync(t => t.TokenAddress == tokenAddress && t.WalletId == walletId);
        }

        public async Task<Token> AddTokenAsync(Token token)
        {
            token.CreatedAt = DateTime.UtcNow;
            _context.Tokens.Add(token);
            await _context.SaveChangesAsync();
            return token;
        }

        public Task UpdateTokenAsync(Token token)
        {
            token.UpdatedAt = DateTime.UtcNow;
            _context.Entry(token).State = EntityState.Modified;
            return _context.SaveChangesAsync();
        }
    }
} 