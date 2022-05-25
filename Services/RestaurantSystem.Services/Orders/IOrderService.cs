﻿namespace RestaurantSystem.Services.Orders
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RestaurantSystem.Data.Models.Orders;
    using RestaurantSystem.Web.ViewModels.Menu;
    using RestaurantSystem.Web.ViewModels.Orders;

    public interface IOrderService
    {
        MenuViewModel GetRestaurantMenuWithUserOrder(string restaurantId, string userId);

        Task<string> MakeOrderAsync(string restaurantId, string userId);

        Order GetUserOrder(string userId, string restaurantId);

       void CompleteOrder(string orderId);

        Task AddProductAsync(string orderId, string productId, string userId, string restaurantId);

        Task RemoveProductAsync(string orderId, string productId, string userId, string restaurantId);

        OrderViewModel GetProductsInOrder(string userId, string restaurantId);

        OrderInputModel SendOrder(string userId, string restaurantId);

        Task<bool> AddOrderInformationАsync(string userId, OrderInputModel orderInput);

        IEnumerable<T> GetAllOrders<T>();

        bool ExstingRestaurant(string restaurantId);

        bool ExstingProduct(string productId);

        bool ExstingOrder(string orderId);
    }
}
