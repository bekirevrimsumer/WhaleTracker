using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WhaleTracker.Core.Entities;
using WhaleTracker.Core.Interfaces;
using WhaleTracker.Core.DTOs;

namespace WhaleTracker.Infrastructure.BackgroundServices
{
    public class WalletMonitoringService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<WalletMonitoringService> _logger;
        private readonly INotificationService _notificationService;

        public WalletMonitoringService(
            IServiceProvider serviceProvider,
            ILogger<WalletMonitoringService> logger,
            INotificationService notificationService)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _notificationService = notificationService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await MonitorWallets(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while monitoring wallets");
                }

                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }

        private async Task MonitorWallets(CancellationToken stoppingToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var walletRepository = scope.ServiceProvider.GetRequiredService<IWalletRepository>();
            var solscanService = scope.ServiceProvider.GetRequiredService<ISolscanService>();

            var activeWallets = await walletRepository.GetAllWalletsAsync();
            foreach (var wallet in activeWallets.Where(w => w.IsActive))
            {
                try
                {
                    var currentTokens = await solscanService.GetWalletTokensAsync(wallet.Address);
                    foreach (var tokenInfo in currentTokens)
                    {
                        var existingToken = await walletRepository.GetTokenByAddressAsync(tokenInfo.TokenAddress, wallet.Id);
                        if (existingToken == null)
                        {
                            var newToken = new Token
                            {
                                WalletId = wallet.Id,
                                TokenAddress = tokenInfo.TokenAddress,
                                TokenName = tokenInfo.TokenName,
                                TokenSymbol = tokenInfo.TokenSymbol,
                                Balance = tokenInfo.Balance,
                                Price = tokenInfo.Price,
                                TokenIcon = tokenInfo.TokenIcon
                            };
                            await walletRepository.AddTokenAsync(newToken);
                            
                            await _notificationService.NotifyTokenAdded(wallet.Id, new TokenNotificationDto
                            {
                                WalletAddress = wallet.Address,
                                TokenSymbol = tokenInfo.TokenSymbol,
                                TokenName = tokenInfo.TokenName,
                                Balance = tokenInfo.Balance
                            });
                        }
                        else if (existingToken.Balance != tokenInfo.Balance || existingToken.Price != tokenInfo.Price)
                        {
                            existingToken.Balance = tokenInfo.Balance;
                            existingToken.Price = tokenInfo.Price;
                            await walletRepository.UpdateTokenAsync(existingToken);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error monitoring wallet {WalletAddress}", wallet.Address);
                }
            }
        }
    }
} 