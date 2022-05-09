namespace RestaurantSystem.Web.ViewModels.Administration.Messages
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using RestaurantSystem.Data.Models.Contacts;
    using RestaurantSystem.Services.Mapping;

    using static RestaurantSystem.Common.GlobalConstants;

    public class ReadMessageViewModel : IMapFrom<AppMessage>
    {
        public string Id { get; init; }

        public string Message { get; init; }

        public string MessageType { get; init; }

        public bool IsOpen { get; init; }

        public IEnumerable<ReplieViewModel> Replies { get; init; }

        [Display(Name = "Саобщението")]
        [Required(ErrorMessage = RequiredFieldMessage)]
        [StringLength(maximumLength: 700, ErrorMessage = LenghtErrorMessage, MinimumLength = 10)]
        public string Text { get; init; }
    }
}
