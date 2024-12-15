using Microsoft.AspNetCore.SignalR;

namespace WhaleTracker.API.Hubs
{
    public class WalletHub : Hub
    {
        public async Task JoinWalletGroup(string walletId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, walletId);
        }

        public async Task LeaveWalletGroup(string walletId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, walletId);
        }
    }
} 