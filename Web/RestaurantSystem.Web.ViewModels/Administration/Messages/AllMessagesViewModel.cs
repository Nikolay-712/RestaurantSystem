namespace RestaurantSystem.Web.ViewModels.Administration.Messages
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static RestaurantSystem.Common.GlobalConstants;

    public class AllMessagesViewModel : PagingViewModel
    {
        public IEnumerable<MessageViewModel> Messages { get; set; }

        public int UnreadMessagesCount { get; set; }

        [Display(Name = "Саобщението")]
        [Required(ErrorMessage = RequiredFieldMessage)]
        [StringLength(maximumLength: 700, ErrorMessage = LenghtErrorMessage, MinimumLength = 10)]
        public string Reply { get; init; }
    }
}
