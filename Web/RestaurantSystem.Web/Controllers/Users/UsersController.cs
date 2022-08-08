namespace RestaurantSystem.Web.Controllers.Users
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RestaurantSystem.Services.Orders;
    using RestaurantSystem.Web.Infrastructure;

    [Authorize]
    public class UsersController : Controller
    {
        private readonly IOrderService orderService;

        public UsersController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        public IActionResult MyOrders(int page = 1)
        {
            var userId = ClaimsPrincipalExtensions.Id(this.User);
            var orders = this.orderService.GetUserOrders(userId, page);

            return this.View(orders);
        }
    }
}
