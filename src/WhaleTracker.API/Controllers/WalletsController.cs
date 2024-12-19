using Microsoft.AspNetCore.Mvc;
using WhaleTracker.Core.DTOs;
using WhaleTracker.Core.Entities;
using WhaleTracker.Core.Interfaces;
using System.Text.Json;

namespace WhaleTracker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalletsController : ControllerBase
    {
        private readonly IWalletRepository _walletRepository;
        private readonly ILogger<WalletsController> _logger;
        private readonly INotificationService _notificationService;

        public WalletsController(
            IWalletRepository walletRepository,
            ILogger<WalletsController> logger,
            INotificationService notificationService)
        {
            _walletRepository = walletRepository;
            _logger = logger;
            _notificationService = notificationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WalletDto>>> GetWallets()
        {
            var wallets = await _walletRepository.GetAllWalletsAsync();
            var walletDtos = wallets.Select(w => new WalletDto
            {
                Id = w.Id,
                Address = w.Address,
                Name = w.Name,
                IsActive = w.IsActive,
                CreatedAt = w.CreatedAt
            });

            return Ok(walletDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WalletDto>> GetWallet(Guid id)
        {
            var wallet = await _walletRepository.GetWalletByIdAsync(id);
            if (wallet == null)
            {
                return NotFound();
            }

            var result = new WalletDto
            {
                Id = wallet.Id,
                Address = wallet.Address,
                Name = wallet.Name,
                IsActive = wallet.IsActive,
                CreatedAt = wallet.CreatedAt,
                Tokens = wallet.Tokens.Select(t => new TokenDto
                {
                    Balance = t.Balance,
                    Price = t.Price,
                    TokenAddress = t.TokenAddress,
                    TokenIcon = t.TokenIcon,
                    TokenName = t.TokenName,
                    TokenSymbol = t.TokenSymbol
                }).ToList()
            };

            if(result.Tokens != null)
                result.Tokens = result.Tokens.OrderBy(x => x.Price).ToList();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<WalletDto>> CreateWallet(CreateWalletDto createWalletDto)
        {
            var existingWallet = await _walletRepository.GetWalletByAddressAsync(createWalletDto.Address);
            if (existingWallet != null)
            {
                return Conflict("Wallet with this address already exists");
            }

            var wallet = new Wallet
            {
                Address = createWalletDto.Address,
                Name = createWalletDto.Name,
                IsActive = createWalletDto.IsActive
            };

            await _walletRepository.AddWalletAsync(wallet);

            return CreatedAtAction(
                nameof(GetWallet),
                new { id = wallet.Id },
                new WalletDto
                {
                    Id = wallet.Id,
                    Address = wallet.Address,
                    Name = wallet.Name,
                    IsActive = wallet.IsActive,
                    CreatedAt = wallet.CreatedAt
                });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWallet(Guid id, UpdateWalletDto updateWalletDto)
        {
            var wallet = await _walletRepository.GetWalletByIdAsync(id);
            if (wallet == null)
            {
                return NotFound();
            }

            wallet.Name = updateWalletDto.Name;
            wallet.IsActive = updateWalletDto.IsActive;

            await _walletRepository.UpdateWalletAsync(wallet);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWallet(Guid id)
        {
            var wallet = await _walletRepository.GetWalletByIdAsync(id);
            if (wallet == null)
            {
                return NotFound();
            }

            await _walletRepository.DeleteWalletAsync(id);

            return NoContent();
        }

        [HttpPost("{walletAddress}/manual-update")]
        public async Task<IActionResult> ManualTokenUpdate(string walletAddress, [FromBody] ManualTokenUpdateDto updateDto)
        {
            try
            {
                var wallet = await _walletRepository.GetWalletByAddressAsync(walletAddress);
                if (wallet == null)
                {
                    return NotFound("Wallet not found");
                }

                var tokenResponse = JsonSerializer.Deserialize<SolscanTokenResponse>(updateDto.TokenData);
                if (tokenResponse?.Data?.Tokens == null)
                {
                    return BadRequest("Invalid token data format");
                }

                var existingTokens = await _walletRepository.GetTokensByWalletIdAsync(wallet.Id);
                var tokenAddressesInJson = tokenResponse.Data.Tokens.Select(t => t.TokenAddress).ToList();

                var tokensToDelete = existingTokens.Where(et => !tokenAddressesInJson.Contains(et.TokenAddress)).ToList();
                foreach (var token in tokensToDelete)
                {
                    await _walletRepository.DeleteTokenAsync(token);
                }

                foreach (var tokenInfo in tokenResponse.Data.Tokens)
                {
                    var existingToken = existingTokens.FirstOrDefault(et => et.TokenAddress == tokenInfo.TokenAddress);
                    if (existingToken == null)
                    {
                        var total = tokenInfo.Balance * (tokenInfo.PriceUsdt.HasValue ? (decimal)tokenInfo.PriceUsdt : (decimal)0.0);

                        if (total > 1) continue;

                        var newToken = new Token
                        {
                            WalletId = wallet.Id,
                            TokenAddress = tokenInfo.TokenAddress,
                            TokenName = tokenInfo.TokenName,
                            TokenSymbol = tokenInfo.TokenSymbol,
                            Balance = tokenInfo.Balance,
                            Price = tokenInfo.PriceUsdt.HasValue ? (decimal)tokenInfo.PriceUsdt : (decimal)0.0,
                            TokenIcon = tokenInfo.TokenIcon
                        };
                        await _walletRepository.AddTokenAsync(newToken);
                    }
                    else
                    {
                        var total = existingToken.Balance * (tokenInfo.PriceUsdt.HasValue ? (decimal)tokenInfo.PriceUsdt : (decimal)0.0);

                        if (total > 1)
                        {
                            await _walletRepository.DeleteTokenAsync(existingToken);
                            continue;
                        }

                        existingToken.Balance = tokenInfo.Balance;
                        existingToken.Price = tokenInfo.PriceUsdt.HasValue ? (decimal)tokenInfo.PriceUsdt : (decimal)0.0;
                        await _walletRepository.UpdateTokenAsync(existingToken);
                    }
                }

                return Ok();
            }
            catch (JsonException)
            {
                return BadRequest("Invalid JSON format");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing manual token update");
                return StatusCode(500, "Internal server error");
            }
        }
    }
} 