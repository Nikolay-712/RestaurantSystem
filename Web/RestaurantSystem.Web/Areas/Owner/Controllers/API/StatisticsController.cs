using Microsoft.AspNetCore.Mvc;
using RestaurantSystem.Services.Statistics;
using RestaurantSystem.Web.ViewModels.Owner.Statistics;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestaurantSystem.Web.Areas.Owner.Controllers.API
{
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
            var statistic = this.statisticService.GenerateRestaurantReport(restaurantId);
            return statistic;
        }
    }
}
