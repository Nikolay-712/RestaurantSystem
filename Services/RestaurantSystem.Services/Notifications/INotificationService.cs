namespace RestaurantSystem.Services.Notifications
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RestaurantSystem.Web.ViewModels.Notifications;

    public interface INotificationService
    {
        Task SendNotificationAsync(
            string userId, string message, string targetId,string notificationType);

        Task ChanageNotificationMessageAsync(
             string userId, string message, string targetId);

        IEnumerable<NotificationViewModel> ShowUserNotifications(string userId);

        bool ChangeNotificationStatus(string notificationId);
    }
}
