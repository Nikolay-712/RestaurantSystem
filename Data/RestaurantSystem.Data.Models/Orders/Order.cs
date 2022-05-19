namespace RestaurantSystem.Data.Models.Orders
{
    using System;
    using System.Collections.Generic;

    using RestaurantSystem.Data.Models.Payments;

    public class Order
    {
        public Order()
        {
            this.Id = Guid.NewGuid().ToString();
            this.OrderProducts = new List<OrderProducts>();
        }

        public string Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public OrderStatus Status { get; set; }

        public string ResaurantId { get; set; }

        public string UserId { get; set; }

        public string PhoneNumber { get; set; }

        public string PaymentId { get; set; }

        public Payment Payment { get; set; }

        public string ShippingAddress { get; set; }

        public ICollection<OrderProducts> OrderProducts { get; set; }
    }
}
