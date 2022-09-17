namespace RestaurantSystem.Web.ViewModels.Payments
{
    using RestaurantSystem.Data.Models.Payments;
    using RestaurantSystem.Services.Mapping;

    public class OnlinePaymentViewModel : IMapFrom<Payment>
    {
        public string Id { get; init; }

        public string CardNumber { get; init; }

        public string CardType { get; init; }

        public string OrderId { get; init; }

        public string CreatedOn { get; init; }

        public bool IsSuccessful { get; init; }

        public decimal Amount { get; init; }
    }
}
