namespace RestaurantSystem.Web.ViewModels.Addresses
{
    using System.ComponentModel.DataAnnotations;

    using static RestaurantSystem.Common.GlobalConstants;

    public class AddresInputModel
    {
        public string Country { get; set; }

        public string Town { get; set; }

        [Display(Name = "Адреса")]
        [Required(ErrorMessage = RequiredFieldMessage)]
        [StringLength(maximumLength: 50, ErrorMessage = LenghtErrorMessage, MinimumLength = 15)]
        public string ShippingAddress { get; set; }
    }
}
