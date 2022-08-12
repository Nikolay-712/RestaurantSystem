namespace RestaurantSystem.Services.Notifications
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using RestaurantSystem.Data;
    using RestaurantSystem.Data.Models.Notifications;
    using RestaurantSystem.Services.Mapping;
    using RestaurantSystem.Web.ViewModels.Notifications;

    public class NotificationService : INotificationService
    {
        private readonly ApplicationDbContext applicationDbContext;

        public NotificationService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task SendNotificationAsync(
            string userId, string message, string targetService, string targetId)
        {
            var notification = new Notification
            {
                CreatedOn = DateTime.Now,
                UserId = userId,
                Message = message,
            };

            if (targetService == "Reservation")
            {
                notification.ReservationId = targetId;
            }

            if (targetService == "Order")
            {
                notification.OrderId = targetId;
            }

            await this.applicationDbContext.Notifications.AddAsync(notification);
            await this.applicationDbContext.SaveChangesAsync();
        }

        public async Task ChanageNotificationMessageAsync(
            string userId, string message, string targetService, string targetId)
        {
            var notifications = this.applicationDbContext
                .Notifications
                .Where(x => x.UserId == userId);

            Notification notification = null;

            if (targetService == "Reservation")
            {
                notification = notifications
                    .FirstOrDefault(x => x.ReservationId == targetId);
            }

            notification.Message = message;
            notification.CreatedOn = DateTime.UtcNow;

            this.applicationDbContext.Update(notification);
            await this.applicationDbContext.SaveChangesAsync();
        }

        public IEnumerable<NotificationViewModel> ShowUserNotifications(string userId)
        {
            var notifications = this.applicationDbContext
                .Notifications
                .Where(x => x.UserId == userId)
                .To<NotificationViewModel>()
                .ToList();

            return notifications;
        }
    }
}
