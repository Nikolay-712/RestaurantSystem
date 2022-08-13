namespace RestaurantSystem.Services.Notifications
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RestaurantSystem.Web.ViewModels.Notifications;

    public interface INotificationService
    {
        Task SendNotificationAsync(
            string userId, string message, string targetService, string targetId);

        Task ChanageNotificationMessageAsync(
             string userId, string message, string targetService, string targetId);

        IEnumerable<NotificationViewModel> ShowUserNotifications(string userId);

        void ChangeNotificationStatus(string notificationId);
    }
}
