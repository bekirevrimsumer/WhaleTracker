using Microsoft.AspNetCore.SignalR;
using WhaleTracker.Core.DTOs;
using WhaleTracker.Core.Interfaces;

namespace WhaleTracker.Infrastructure.Services
{
    public class SignalRNotificationService : INotificationService
    {
        private readonly IHubContext<Hub> _hubContext;

        public SignalRNotificationService(IHubContext<Hub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task NotifyTokenAdded(Guid walletId, TokenNotificationDto notification)
        {
            await _hubContext.Clients.Group(walletId.ToString())
                .SendAsync("TokenAdded", notification);
        }
    }
} 