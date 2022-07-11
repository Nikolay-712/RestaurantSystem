namespace RestaurantSystem.Web.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using RestaurantSystem.Services.Seeder;
    using RestaurantSystem.Web.ViewModels;

    public class HomeController : Controller
    {
        private readonly IDataSeeder dataSeeder;

        public HomeController(IDataSeeder dataSeeder)
        {
            this.dataSeeder = dataSeeder;
        }

        public async Task<IActionResult> Index()
        {
            if (!this.dataSeeder.CheckDataBase())
            {
                await this.dataSeeder.AddRestaurantAsync();
            }

            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
