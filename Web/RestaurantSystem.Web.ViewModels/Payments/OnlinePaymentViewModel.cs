namespace RestaurantSystem.Web.ViewModels.Payments
{
    using System;
    using RestaurantSystem.Data.Models.Orders;
    using RestaurantSystem.Data.Models.Payments;
    using RestaurantSystem.Services.Mapping;

    public class OnlinePaymentViewModel : IMapFrom<Payment>
    {
        public string OrderId { get; init; }

        public string CreatedOn { get; init; }

        public bool IsSuccessful { get; init; }

        public decimal Amount { get; init; }
    }
}
