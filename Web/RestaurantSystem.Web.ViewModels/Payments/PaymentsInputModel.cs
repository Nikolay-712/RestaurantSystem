using RestaurantSystem.Data.Models.Payments;
using System;

namespace RestaurantSystem.Web.ViewModels.Payments
{
    public class PaymentsInputModel
    {
        public string CardNumber { get; init; }

        public string CardName { get; init; }

        public DateTime Expiration { get; init; }

        public string CVV { get; init; }

        public PaymentType PaymentType { get; init; }
    }
}
