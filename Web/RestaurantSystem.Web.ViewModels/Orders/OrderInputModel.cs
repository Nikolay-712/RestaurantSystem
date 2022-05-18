namespace RestaurantSystem.Web.ViewModels.Orders
{
    using System.ComponentModel.DataAnnotations;

    using RestaurantSystem.Web.ViewModels.Addresses;
    using RestaurantSystem.Web.ViewModels.CustomValidation;
    using RestaurantSystem.Web.ViewModels.Payments;

    using static RestaurantSystem.Common.GlobalConstants;

    public class OrderInputModel : OrderViewModel
    {
        public string OrderId { get; init; }

        [Required(ErrorMessage = RequiredFieldMessage)]
        [PhoneValidation]
        public string PhoneNumber { get; init; }

        public PaymentsInputModel Payment { get; init; }

        public AddresInputModel Addres { get; init; }

        public bool CashMethod { get; init; }

        public bool DebitMethod { get; init; }

        public bool SaveAddress { get; init; }
    }
}
