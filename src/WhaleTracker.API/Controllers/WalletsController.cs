using Microsoft.AspNetCore.Mvc;
using WhaleTracker.Core.DTOs;
using WhaleTracker.Core.Entities;
using WhaleTracker.Core.Interfaces;

namespace WhaleTracker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalletsController : ControllerBase
    {
        private readonly IWalletRepository _walletRepository;
        private readonly ILogger<WalletsController> _logger;

        public WalletsController(
            IWalletRepository walletRepository,
            ILogger<WalletsController> logger)
        {
            _walletRepository = walletRepository;
            _logger = logger;
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

            return Ok(new WalletDto
            {
                Id = wallet.Id,
                Address = wallet.Address,
                Name = wallet.Name,
                IsActive = wallet.IsActive,
                CreatedAt = wallet.CreatedAt
            });
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
    }
} 