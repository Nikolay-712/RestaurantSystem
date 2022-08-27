namespace RestaurantSystem.Services.Statistics
{
    using RestaurantSystem.Web.ViewModels.Owner.Statistics;

    public interface IStatisticService
    {
        StatisticViewModel GenerateRestaurantReport(string restaurantId, string ownerId);
    }
}
