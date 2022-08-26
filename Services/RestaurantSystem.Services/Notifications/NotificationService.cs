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
            string userId, string message, string targetId, string notificationType)
        {
            var existingNotification =
                this.applicationDbContext
                .Notifications
                .Where(x => x.UserId == userId)
                .FirstOrDefault(x => x.TargetId == targetId);

            if (existingNotification != null)
            {
                existingNotification.CreatedOn = DateTime.Now;
                existingNotification.Message = message;
                existingNotification.IsSeen = false;

                this.applicationDbContext.Update(existingNotification);
                await this.applicationDbContext.SaveChangesAsync();
            }
            else
            {
                var notification = new Notification
                {
                    CreatedOn = DateTime.Now,
                    UserId = userId,
                    TargetId = targetId,
                    Message = message,
                    NotificationType = notificationType,
                };

                await this.applicationDbContext.Notifications.AddAsync(notification);
                await this.applicationDbContext.SaveChangesAsync();
            }
        }

        public async Task ChanageNotificationMessageAsync(
            string userId, string message, string targetId)
        {
            var notification = this.applicationDbContext
                .Notifications
                .Where(x => x.UserId == userId)
                .FirstOrDefault(x => x.TargetId == targetId);

            notification.Message = message;
            notification.CreatedOn = DateTime.Now;
            notification.IsSeen = false;

            this.applicationDbContext.Update(notification);
            await this.applicationDbContext.SaveChangesAsync();
        }

        public IEnumerable<NotificationViewModel> ShowUserNotifications(string userId)
        {
            var notifications = this.applicationDbContext
                .Notifications
                .Where(x => x.UserId == userId)
                .To<NotificationViewModel>()
                .ToList()
                .OrderByDescending(x => x.CreatedOn);

            return notifications;
        }

        public bool ChangeNotificationStatus(string notificationId)
        {
            var notification = this.applicationDbContext
                .Notifications
                .FirstOrDefault(x => x.Id == notificationId);

            if (notification == null)
            {
                return false;
            }

            notification.IsSeen = true;

            this.applicationDbContext.Notifications.Update(notification);
            this.applicationDbContext.SaveChanges();

            return true;
        }
    }
}
