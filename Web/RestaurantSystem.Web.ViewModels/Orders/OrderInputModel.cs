namespace RestaurantSystem.Web.ViewModels.Orders
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using RestaurantSystem.Web.ViewModels.Addresses;
    using RestaurantSystem.Web.ViewModels.CustomValidation;
    using RestaurantSystem.Web.ViewModels.Menu;
    using RestaurantSystem.Web.ViewModels.Payments;

    using static RestaurantSystem.Common.GlobalConstants;

    public class OrderInputModel : OrderViewModel
    {
        public string OrderId { get; set; }

        public string RestaurantId { get; set; }

        [Required(ErrorMessage = RequiredFieldMessage)]
        [PhoneValidation]
        public string PhoneNumber { get; set; }

        public PaymentsInputModel Payment { get; set; }

        public AddresInputModel Addres { get; set; }

        public bool SaveAddress { get; init; }

        public IEnumerable<ProductViewModel> Addons { get; set; }
    }
}
