using WhaleTracker.Core.DTOs;

namespace WhaleTracker.Core.Interfaces
{
    public interface INotificationService
    {
        Task NotifyTokenAdded(Guid walletId, TokenNotificationDto notification);
    }
} 