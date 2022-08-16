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

            var aa = this.BestSellersPreoducts(restaurantId);

            return restaurant;
        }

        public IEnumerable<ProductStatisticViewModel> BestSellersPreoducts(string restaurantId)
        {
            var products = this.applicationDbContext.Products
                .Where(x => x.RestaurantId == restaurantId)
                .Select(x => new ProductStatisticViewModel
                {
                    Name = x.Name,
                    InOrders = x.OrderProducts.Count(),
                    Ordered = x.OrderProducts.Select(x => x.Count).Sum(),
                    Rating = x.Ratings.Select(x => x.Stars),
                })
                .OrderByDescending(x => x.Ordered)
                .ToList();

            return products;
        }

    }
}
