namespace RestaurantSystem.Web.ViewModels.Owner.Restaurants
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using RestaurantSystem.Web.ViewModels.CustomValidation;

    using static RestaurantSystem.Common.GlobalConstants;

    public class RegisterRestaurantInputModel
    {
        [Display(Name = "Името")]
        [Required(ErrorMessage = RequiredFieldMessage)]
        [StringLength(maximumLength: 30, ErrorMessage = LenghtErrorMessage, MinimumLength = 5)]
        [ExistingName]
        public string Name { get; init; }

        [Display(Name = "Описанието")]
        [Required(ErrorMessage = RequiredFieldMessage)]
        [StringLength(maximumLength: 200, ErrorMessage = LenghtErrorMessage, MinimumLength = 30)]
        public string Description { get; init; }

        [Display(Name = "Цената")]
        [Required(ErrorMessage = RequiredFieldMessage)]
        [Range(minimum: 0.1, maximum: 5.0, ErrorMessage = PriceErrorMessage)]
        public decimal DeliveryPeice { get; init; }

        [Required(ErrorMessage = RequiredFieldMessage)]
        [TimeFormat]
        public string OpenIn { get; init; }

        [Required(ErrorMessage = RequiredFieldMessage)]
        [TimeFormat]
        public string CloseIn { get; init; }
    }
}
