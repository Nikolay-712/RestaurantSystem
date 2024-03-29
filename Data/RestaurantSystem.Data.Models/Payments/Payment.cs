﻿namespace RestaurantSystem.Data.Models.Payments
{
    using System;

    public class Payment
    {
        public Payment()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string CardNumber { get; set; }

        public string CardType { get; set; }

        public DateTime CreatedOn { get; set; }

        public PaymentType PaymentType { get; set; }

        public bool IsSuccessful { get; set; }

        public string OrderId { get; set; }

        public string UserId { get; set; }

        public decimal Amount { get; set; }
    }
}
