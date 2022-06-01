namespace RestaurantSystem.Web.Controllers.Restaurants
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RestaurantSystem.Services.Orders;
    using RestaurantSystem.Services.Ratings;
    using RestaurantSystem.Services.Restaurants;
    using RestaurantSystem.Web.Infrastructure;
    using RestaurantSystem.Web.ViewModels.Orders;
    using RestaurantSystem.Web.ViewModels.Restaurants;

    public class RestaurantsController : Controller
    {
        private readonly IRestaurantService restaurantService;
        private readonly IOrderService orderService;
        private readonly IRatingService ratingService;

        public RestaurantsController(
            IRestaurantService restaurantService,
            IOrderService orderService,
            IRatingService ratingService)
        {
            this.restaurantService = restaurantService;
            this.orderService = orderService;
            this.ratingService = ratingService;
        }

        public IActionResult Index()
        {
            var restaurants = this.restaurantService
                .AllRestaurants<RestaurantInfoViewModel>();

            return this.View(restaurants);
        }

        public IActionResult Menu(string restaurantId, string category)
        {
            var userId = ClaimsPrincipalExtensions.Id(this.User);
            var menu = this.orderService.GetRestaurantMenuWithUserOrder(restaurantId, category, userId);

            if (menu == null)
            {
                return this.NotFound();
            }

            return this.View(menu);
        }

        [Authorize]
        public async Task<IActionResult> Order(string restaurantId, string category, string productId)
        {
            if (!this.orderService.ExstingRestaurant(restaurantId)
                || !this.orderService.ExstingProduct(productId))
            {
                return this.NotFound();
            }

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

            return this.RedirectToAction("Menu", new { restaurantId = restaurantId, category = category });
        }

        [Authorize]
        public async Task<IActionResult> ProductCount(string restaurantId, string productId, string category, string orderId, string count)
        {
            if (!this.orderService.ExstingRestaurant(restaurantId)
                || !this.orderService.ExstingProduct(productId)
                || !this.orderService.ExstingOrder(orderId))
            {
                return this.NotFound();
            }

            var userId = ClaimsPrincipalExtensions.Id(this.User);

            if (count == "+")
            {
                await this.orderService.AddProductAsync(orderId, productId, userId, restaurantId);
            }
            else if (count == "-")
            {
                await this.orderService.RemoveProductAsync(orderId, productId, userId, restaurantId);
            }
            else
            {
                return this.NotFound();
            }

            return this.RedirectToAction("Menu", new { restaurantId = restaurantId, category = category });
        }

        [Authorize]
        public IActionResult SendOrder(string restaurantId)
        {
            var userId = ClaimsPrincipalExtensions.Id(this.User);
            var order = this.orderService.SendOrder(userId, restaurantId);

            if (order == null)
            {
                return this.NotFound();
            }

            return this.View(order);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SendOrder(OrderInputModel orderInput, string restaurantId)
        {
            var userId = ClaimsPrincipalExtensions.Id(this.User);

            if (!this.ModelState.IsValid)
            {
                var order = this.orderService.SendOrder(userId, restaurantId);
                return this.View(order);
            }

            var result = await this.orderService.AddOrderInformationАsync(userId, orderInput);

            if (!result)
            {
                return this.NotFound();
            }

            var message = "Благодарим за вашата поръчка";
            this.TempData["order"] = message;

            return this.RedirectToAction("Menu", new { restaurantId = restaurantId });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Rate(string restaurantId, int rating)
        {
            var userId = ClaimsPrincipalExtensions.Id(this.User);
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();

            await this.ratingService.AddRateAsync(restaurantId, rating, userId, controllerName);

            return View();
        }

    }
}
