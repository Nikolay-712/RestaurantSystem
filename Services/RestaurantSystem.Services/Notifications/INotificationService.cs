namespace RestaurantSystem.Services.Notifications
{
    using System.Threading.Tasks;

    public interface INotificationService
    {
        Task SendNotificationAsync(
            string userId, string message, string targetService, string targetId);

        Task ChanageNotificationMessageAsync(
             string userId, string message, string targetService, string targetId);
    }
}
