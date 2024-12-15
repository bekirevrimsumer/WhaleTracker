using Microsoft.EntityFrameworkCore;
using WhaleTracker.Core.Entities;

namespace WhaleTracker.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Token> Tokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Token>()
                .HasOne(t => t.Wallet)
                .WithMany(w => w.Tokens)
                .HasForeignKey(t => t.WalletId);
        }
    }
} 