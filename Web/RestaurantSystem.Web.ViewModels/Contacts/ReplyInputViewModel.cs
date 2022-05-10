namespace RestaurantSystem.Web.ViewModels.Contacts
{
    using System.ComponentModel.DataAnnotations;

    using static RestaurantSystem.Common.GlobalConstants;

    public class ReplyInputViewModel
    {
        [Display(Name = "Саобщението")]
        [Required(ErrorMessage = RequiredFieldMessage)]
        [StringLength(maximumLength: 700, ErrorMessage = LenghtErrorMessage, MinimumLength = 10)]
        public string Text { get; init; }
    }
}
