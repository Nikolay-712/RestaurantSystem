namespace RestaurantSystem.Web.Areas.Owner.Controllers.Orders
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using RestaurantSystem.Services.Orders;
    using RestaurantSystem.Web.ViewModels.Owner.Orders;

    public class OrdersController : OwnerController
    {
        private readonly IOrderService orderService;
        private const int OrdersPerpage = 5;

        public OrdersController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        public IActionResult Index(string restaurantId, int page = 1)
        {
            var allOrdersForRestaurant = this.orderService
                    .GetAllOrders<OrderViewModel>()
                    .Where(x => x.ResaurantId == restaurantId)
                    .Where(x => x.Status != "Pending")
                    .Where(x => x.Status != "InProgre");


            var allOrders = new AllOrdersViewModel
            {
                ItemsPerPage = OrdersPerpage,
                ItemsCount = allOrdersForRestaurant.Count(),
                PageNumber = page,
                AllOrders = allOrdersForRestaurant
                    .OrderByDescending(x => x.CreatedOn)
                    .Skip((page - 1) * OrdersPerpage)
                    .Take(OrdersPerpage).ToList(),
                PendingOrders = this.orderService
                    .GetAllOrders<OrderViewModel>()
                    .Where(x => x.ResaurantId == restaurantId)
                    .OrderBy(x => x.CreatedOn)
                    .Where(x => x.Status == "Pending"),
                AllOrdersCount = allOrdersForRestaurant.Count(),
            };

            return this.View(allOrders);
        }

        public IActionResult Details(string orderId)
        {
            var order = this.orderService
                .GetAllOrders<OrderViewModel>()
                .FirstOrDefault(x => x.Id == orderId);

            return this.View(order);
        }

        [HttpPost]
        public IActionResult SendOrder(string orderId, string restaurantId)
        {
            this.orderService.CompleteOrder(orderId);

            return this.RedirectToAction("Index", new { restaurantId = restaurantId });
        }
    }
}
