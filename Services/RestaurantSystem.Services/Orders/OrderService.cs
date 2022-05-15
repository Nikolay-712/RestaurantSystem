namespace RestaurantSystem.Services.Orders
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using RestaurantSystem.Data;
    using RestaurantSystem.Data.Models.Orders;
    using RestaurantSystem.Services.Mapping;
    using RestaurantSystem.Web.ViewModels.Orders;

    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext applicationDbContext;

        public OrderService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
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
            var aa = this.applicationDbContext
                .OrderProducts
                .Where(x => x.OrderId == orderId)
                .To<OrderProductViewModel>();



            var product = this.applicationDbContext
                .Products
                .Where(x => x.RestaurantId == restaurantId)
                .FirstOrDefault(x => x.Id == productId);

            var order = this.GetUserOrder(userId, restaurantId);

            var currentOrderProducts = this.applicationDbContext.
                OrderProducts.
                FirstOrDefault(x => x.ProductId == product.Id && x.OrderId == orderId);

            if (currentOrderProducts == null)
            {
                var orderProducts = new OrderProducts
                {
                    OrderId = order.Id,
                    ProductId = productId,
                    Count = 1,
                };

                await this.applicationDbContext.OrderProducts.AddAsync(orderProducts);
            }
            else
            {
                currentOrderProducts.Count = currentOrderProducts.Count + 1;
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

        public OrderViewModel OrdersProducts(string userId, string restaurantId)
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
    }
}