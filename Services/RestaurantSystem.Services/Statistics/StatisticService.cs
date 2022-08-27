namespace RestaurantSystem.Services.Statistics
{
    using System.Linq;

    using RestaurantSystem.Data;
    using RestaurantSystem.Services.Mapping;
    using RestaurantSystem.Web.ViewModels.Owner.Statistics;

    public class StatisticService : IStatisticService
    {
        private readonly ApplicationDbContext applicationDbContext;

        public StatisticService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public StatisticViewModel GenerateRestaurantReport(string restaurantId, string ownerId)
        {
            var statistic = this.applicationDbContext
                .Restaurants
                .Where(x => x.OwnerId == ownerId)
                .Where(x => x.Id == restaurantId)
                .To<StatisticViewModel>()
                .FirstOrDefault();

            return statistic;
        }
    }
}
