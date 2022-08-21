namespace RestaurantSystem.Services.Statistics
{
    using System;
    using System.Linq;

    using Microsoft.Extensions.Caching.Memory;
    using RestaurantSystem.Common;
    using RestaurantSystem.Data;
    using RestaurantSystem.Services.Mapping;
    using RestaurantSystem.Web.ViewModels.Owner.Statistics;

    public class StatisticService : IStatisticService
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IMemoryCache memoryCache;

        public StatisticService(
            ApplicationDbContext applicationDbContext,
            IMemoryCache memoryCache)
        {
            this.applicationDbContext = applicationDbContext;
            this.memoryCache = memoryCache;
        }

        public StatisticViewModel GenerateRestaurantReport(string restaurantId)
        {
            var statistic = this.memoryCache.Get<StatisticViewModel>(GlobalConstants.StatisticCacheKey);

            if (statistic == null)
            {
                statistic = this.applicationDbContext
                .Restaurants
                .Where(x => x.Id == restaurantId)
                .To<StatisticViewModel>()
                .FirstOrDefault();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromDays(5));
                this.memoryCache
                    .Set(GlobalConstants.StatisticCacheKey, statistic, cacheOptions);
            }

            return statistic;
        }
    }
}
