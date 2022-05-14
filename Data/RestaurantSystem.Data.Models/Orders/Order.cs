namespace RestaurantSystem.Data.Models.Orders
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using RestaurantSystem.Data.Models.Payments;
    using RestaurantSystem.Data.Models.Products;

    public class Order
    {
        public Order()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Products = new Dictionary<Product, int>();
        }

        public string Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public OrderStatus Status { get; set; }

        [NotMapped]
        public IDictionary<Product, int> Products { get; set; }

        public string ResaurantId { get; set; }

        public string UserId { get; set; }

        public string PaymentId { get; set; }

        public Payment Payment { get; set; }
    }
}
