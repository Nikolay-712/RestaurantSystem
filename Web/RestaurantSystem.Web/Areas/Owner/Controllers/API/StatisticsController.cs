namespace RestaurantSystem.Web.Areas.Owner.Controllers.API
{
    using Microsoft.AspNetCore.Mvc;
    using RestaurantSystem.Services.Statistics;
    using RestaurantSystem.Web.Infrastructure;
    using RestaurantSystem.Web.ViewModels.Owner.Statistics;

    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticService statisticService;

        public StatisticsController(IStatisticService statisticService)
        {
            this.statisticService = statisticService;
        }

        public StatisticViewModel Get(string restaurantId)
        {
            var statistic = this.statisticService
                .GenerateRestaurantReport(restaurantId, ClaimsPrincipalExtensions.Id(this.User));

            return statistic;
        }
    }
}
