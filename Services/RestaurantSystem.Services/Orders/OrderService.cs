namespace RestaurantSystem.Services.Orders
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using RestaurantSystem.Data;
    using RestaurantSystem.Data.Models.Orders;
    using RestaurantSystem.Services.Mapping;
    using RestaurantSystem.Services.Restaurants;
    using RestaurantSystem.Web.ViewModels.Menu;
    using RestaurantSystem.Web.ViewModels.Orders;

    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IRestaurantService restaurantService;

        public OrderService(ApplicationDbContext applicationDbContext, IRestaurantService restaurantService)
        {
            this.applicationDbContext = applicationDbContext;
            this.restaurantService = restaurantService;
        }

        public MenuViewModel GetRestaurantMenuWithUserOrder(string restaurantId, string userId)
        {
            var menu = this.restaurantService
                   .AllRestaurants<MenuViewModel>()
                   .FirstOrDefault(x => x.Id == restaurantId);

            if (menu == null)
            {
                return menu;
            }

            menu.Order = this.GetProductsInOrder(userId, restaurantId);

            return menu;
        }

        public async Task<string> MakeOrderAsync(string restaurantId, string userId)
        {
            var order = new Order
            {
                CreatedOn = DateTime.Now,
                Status = OrderStatus.InProgre,
                PaymentId = "NotSelected",
                ResaurantId = restaurantId,
                UserId = userId,
            };

            await this.applicationDbContext.Orders.AddAsync(order);
            await this.applicationDbContext.SaveChangesAsync();

            return order.Id;
        }

        public async Task AddProductAsync(string orderId, string productId, string userId, string restaurantId)
        {
            var currentOrderProducts = this.applicationDbContext.
                OrderProducts.
                FirstOrDefault(x => x.ProductId == productId && x.OrderId == orderId);

            if (currentOrderProducts == null)
            {
                var orderProducts = new OrderProducts
                {
                    OrderId = orderId,
                    ProductId = productId,
                    Count = 1,
                };

                await this.applicationDbContext.OrderProducts.AddAsync(orderProducts);
            }
            else
            {
                currentOrderProducts.Count++;
                this.applicationDbContext.OrderProducts.Update(currentOrderProducts);
            }

            await this.applicationDbContext.SaveChangesAsync();
        }

        public async Task RemoveProductAsync(string orderId, string productId, string userId, string restaurantId)
        {
            var currentOrderProducts = this.applicationDbContext.
                OrderProducts.
                FirstOrDefault(x => x.ProductId == productId && x.OrderId == orderId);

            if (currentOrderProducts.Count == 1)
            {
                this.applicationDbContext.Remove(currentOrderProducts);
            }
            else
            {
                currentOrderProducts.Count--;
                this.applicationDbContext.OrderProducts.Update(currentOrderProducts);
            }

            await this.applicationDbContext.SaveChangesAsync();
        }

        public Order GetUserOrder(string userId, string restaurantId)
        {
            var order = this.applicationDbContext
                .Orders
                .Where(x => x.UserId == userId)
                .Where(x => x.ResaurantId == restaurantId)
                .FirstOrDefault(x => x.Status == OrderStatus.InProgre);

            return order;
        }

        public OrderViewModel GetProductsInOrder(string userId, string restaurantId)
        {
            var orders = new OrderViewModel
            {
                Products = this.applicationDbContext
                    .OrderProducts
                    .Where(x => x.Order.Status == OrderStatus.InProgre)
                    .Where(x => x.Order.ResaurantId == restaurantId)
                    .Where(x => x.Order.UserId == userId)
                    .To<OrderProductViewModel>()
                    .ToList(),
            };

            return orders;
        }

        public bool ExstingRestaurant(string restaurantId)
        {
            return this.applicationDbContext.Restaurants.Any(x => x.Id == restaurantId);
        }

        public bool ExstingProduct(string productId)
        {
            return this.applicationDbContext.Products.Any(x => x.Id == productId);
        }

        public bool ExstingOrder(string orderId)
        {
            return this.applicationDbContext.Orders.Any(x => x.Id == orderId);
        }
    }
}
