using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using WhaleTracker.Core.Interfaces;
using WhaleTracker.Core.DTOs;

namespace WhaleTracker.Infrastructure.Services
{
    public class SolscanService : ISolscanService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public SolscanService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://pro-api.solscan.io/v2/");
            _apiKey = configuration["SolscanApi:ApiKey"] ?? throw new ArgumentNullException("Solscan API key is missing");
            
            _httpClient.DefaultRequestHeaders.Add("token", _apiKey);
        }

        public async Task<IEnumerable<TokenDto>> GetWalletTokensAsync(string walletAddress)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<SolscanTokenResponse>($"account/tokens?address={walletAddress}");
                
                if (response?.Success != true)
                    return Enumerable.Empty<TokenDto>();

                return response.Data.Tokens.Select(t => new TokenDto
                {
                    Price = t.PriceUsdt,
                    TokenSymbol = t.TokenSymbol,
                    TokenAddress = t.TokenAddress,
                    Balance = t.Balance,
                    TokenName = t.TokenName,
                    TokenIcon = t.TokenIcon
                });
            }
            catch (Exception)
            {
                // Hata loglama eklenebilir
                return Enumerable.Empty<TokenDto>();
            }
        }
    }
} 