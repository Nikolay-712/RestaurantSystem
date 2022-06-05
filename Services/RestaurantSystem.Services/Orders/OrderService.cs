namespace RestaurantSystem.Services.Orders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using RestaurantSystem.Data;
    using RestaurantSystem.Data.Models.Orders;
    using RestaurantSystem.Services.Mapping;
    using RestaurantSystem.Services.Menu;
    using RestaurantSystem.Services.Payments;
    using RestaurantSystem.Services.Users;
    using RestaurantSystem.Web.ViewModels.Addresses;
    using RestaurantSystem.Web.ViewModels.Menu;
    using RestaurantSystem.Web.ViewModels.Orders;

    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IPaymentService paymentService;
        private readonly IUserService userService;
        private readonly IMenuService menuService;

        public OrderService(
            ApplicationDbContext applicationDbContext,
            IPaymentService paymentService,
            IUserService userService,
            IMenuService menuService)
        {
            this.applicationDbContext = applicationDbContext;
            this.paymentService = paymentService;
            this.userService = userService;
            this.menuService = menuService;
        }

        public MenuViewModel GetRestaurantMenuWithUserOrder(string restaurantId, string category, string userId)
        {
            var menu = this.menuService.ShowRestaurantMenu(restaurantId, category, userId);
            if (menu != null) { menu.Order = this.GetProductsInOrder(userId, restaurantId); }

            return menu;
        }

        public async Task<string> MakeOrderAsync(string restaurantId, string userId)
        {
            var user = await this.userService.GetUserByIdAsync(userId);

            var phoneNumber = user.PhoneNumber == null ? string.Empty : user.PhoneNumber;
            var address = this.userService.GetUserAddress(userId);
            var shippingAddress = string.Empty;

            if (address != null)
            {
                shippingAddress = address.ShippingAddress == null ? string.Empty : address.ShippingAddress;
            }

            var order = new Order
            {
                CreatedOn = DateTime.Now,
                Status = OrderStatus.InProgre,
                PaymentId = "NotSelected",
                ShippingAddress = shippingAddress,
                PhoneNumber = phoneNumber,
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

        public void CompleteOrder(string orderId)
        {
            var order = this.applicationDbContext
                .Orders.
                FirstOrDefault(x => x.Id == orderId);

            order.Status = OrderStatus.Sent;

            this.applicationDbContext.Update(order);
            this.applicationDbContext.SaveChanges();
        }

        public OrderViewModel GetProductsInOrder(string userId, string restaurantId)
        {
            var order = this.applicationDbContext
                 .Orders
                 .Where(x => x.Status == OrderStatus.InProgre)
                 .Where(x => x.ResaurantId == restaurantId)
                 .Where(x => x.UserId == userId)
                 .To<OrderViewModel>().FirstOrDefault();

            if (order != null)
            {
                order.TotaalSum = order.OrderProducts.Select(x => x.Sum).Sum() + this.GetDeliveryPrice(restaurantId);
            }

            return order;
        }

        public OrderInputModel SendOrder(string userId, string restaurantId)
        {
            var order = this.GetUserOrder(userId, restaurantId);
            var address = this.userService.GetUserAddress(userId);

            if (order == null)
            {
                return null;
            }

            var inputOrder = new OrderInputModel();

            if (order.ShippingAddress != string.Empty)
            {
                var inputAddress = new AddresInputModel()
                {
                    Country = address.Country,
                    Town = address.Town,
                    ShippingAddress = address.ShippingAddress,
                };

                inputOrder.Addres = inputAddress;
            }
            else
            {
                inputOrder.Addres = new AddresInputModel();
            }

            var orderViewModel = this.GetProductsInOrder(userId, restaurantId);

            inputOrder.OrderId = order.Id;
            inputOrder.PhoneNumber = order.PhoneNumber;
            inputOrder.TotaalSum = orderViewModel.TotaalSum;
            inputOrder.OrderProducts = orderViewModel.OrderProducts;

            return inputOrder;
        }

        public async Task<bool> AddOrderInformationАsync(string userId, OrderInputModel orderInput)
        {
            var order = this.GetUserOrder(userId, orderInput.RestaurantId);

            if (!this.ExstingOrder(orderInput.OrderId))
            {
                return false;
            }

            var paymentId = await this.paymentService
                .MakePaymentAsync(orderInput.OrderId, orderInput.Payment);

            if (paymentId == null)
            {
                return false;
            }

            order.Status = OrderStatus.Pending;
            order.CreatedOn = DateTime.Now;
            order.PaymentId = paymentId;
            order.ShippingAddress = orderInput.Addres.ShippingAddress;
            order.PhoneNumber = orderInput.PhoneNumber;

            if (orderInput.SaveAddress)
            {
                await this.userService.SaveAddressAsync(userId, orderInput.Addres);
                await this.userService.SavePhoneNumberAsync(userId, orderInput.PhoneNumber);
            }

            this.applicationDbContext.Update(order);
            await this.applicationDbContext.SaveChangesAsync();

            return true;
        }

        public IEnumerable<T> GetAllOrders<T>()
        {
            return this.applicationDbContext.Orders.To<T>();
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

        private decimal GetDeliveryPrice(string restaurantId)
        {
            return this.applicationDbContext
                .Restaurants
                .FirstOrDefault(x => x.Id == restaurantId)
                .DeliveryPeice;
        }
    }
}
