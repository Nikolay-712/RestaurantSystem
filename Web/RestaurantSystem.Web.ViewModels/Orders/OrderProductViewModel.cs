﻿namespace RestaurantSystem.Web.ViewModels.Orders
{
    using RestaurantSystem.Data.Models.Orders;
    using RestaurantSystem.Services.Mapping;

    public class OrderProductViewModel : IMapFrom<OrderProducts>
    {
        public string OrderId { get; set; }

        public string ProductId { get; init; }

        public string ProductName { get; init; }

        public decimal ProductPrice { get; init; }

        public int Count { get; init; }

        public decimal Sum => this.ProductPrice * this.Count;
    }
}
