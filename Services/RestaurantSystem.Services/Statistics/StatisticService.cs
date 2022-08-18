namespace RestaurantSystem.Services.Statistics
{
    using System.Collections.Generic;
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

        public StatisticViewModel GenerateRestaurantReport(string restaurantId)
        {
            var restaurant = this.applicationDbContext
                .Restaurants
                .Where(x => x.Id == restaurantId)
                .To<StatisticViewModel>()
                .FirstOrDefault();

            return restaurant;
        }
    }
}
