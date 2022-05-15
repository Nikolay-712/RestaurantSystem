namespace RestaurantSystem.Web.Controllers.Restaurants
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RestaurantSystem.Services.Orders;
    using RestaurantSystem.Services.Restaurants;
    using RestaurantSystem.Web.Infrastructure;
    using RestaurantSystem.Web.ViewModels.Menu;
    using RestaurantSystem.Web.ViewModels.Restaurants;

    public class RestaurantsController : Controller
    {
        private readonly IRestaurantService restaurantService;
        private readonly IOrderService orderService;

        public RestaurantsController(IRestaurantService restaurantService, IOrderService orderService)
        {
            this.restaurantService = restaurantService;
            this.orderService = orderService;
        }

        public IActionResult Index()
        {
            var restaurants = this.restaurantService
                .AllRestaurants<RestaurantInfoViewModel>();

            return this.View(restaurants);
        }

        public IActionResult Menu(string restaurantId)
        {
            var userId = ClaimsPrincipalExtensions.Id(this.User);
            var menu = this.restaurantService
                   .AllRestaurants<MenuViewModel>()
                   .FirstOrDefault(x => x.Id == restaurantId);

            menu.Order = this.orderService.OrdersProducts(userId, restaurantId);

            return this.View(menu);
        }

        [Authorize]
        public async Task<IActionResult> Order(string restaurantId, string productId)
        {
            var userId = ClaimsPrincipalExtensions.Id(this.User);
            var currentOrder = this.orderService.GetUserOrder(userId, restaurantId);

            if (currentOrder == null)
            {
                var orderId = await this.orderService.MakeOrderAsync(restaurantId, userId);
                await this.orderService.AddProductAsync(orderId, productId, userId, restaurantId);
            }
            else
            {
                await this.orderService.AddProductAsync(currentOrder.Id, productId, userId, restaurantId);
            }

            return this.RedirectToAction("Menu", new { restaurantId = restaurantId });
        }
    }
}
