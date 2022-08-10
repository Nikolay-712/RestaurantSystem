namespace RestaurantSystem.Web.ViewModels.Reservations
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using RestaurantSystem.Web.ViewModels.CustomValidation;

    using static RestaurantSystem.Common.GlobalConstants;

    public class ReservationInputViewModel
    {
        [Display(Name = "Името")]
        [Required(ErrorMessage = RequiredFieldMessage)]
        [StringLength(maximumLength: 30, ErrorMessage = LenghtErrorMessage, MinimumLength = 3)]
        public string FirstName { get; init; }

        [Required(ErrorMessage = RequiredFieldMessage)]
        [PhoneValidation]
        public string PhoneNumber { get; init; }

        [Required(ErrorMessage = RequiredFieldMessage)]
        [DateValidation]
        public DateTime Date { get; init; }

        [Required(ErrorMessage = RequiredFieldMessage)]
        public string Time { get; init; }

        [Display(Name = "Бройката")]
        [Required(ErrorMessage = RequiredFieldMessage)]
        [Range(minimum: 1, maximum: 100, ErrorMessage = CountErrorMessage)]
        public int PeopleCount { get; init; }

        [Required]
        public string RestaurantId { get; init; }

        public bool SavePhone { get; init; }
    }
}
