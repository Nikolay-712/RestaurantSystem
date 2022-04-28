namespace RestaurantSystem.Web.ViewModels.Administration.Messages
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using RestaurantSystem.Data.Models.Contacts;
    using RestaurantSystem.Services.Mapping;

    using static RestaurantSystem.Common.GlobalConstants;

    public class AdminMessageViewModel : IMapFrom<ApplicationMessage>
    {
        public string Id { get; set; }

        public DateTime CreatedOn { get; init; }

        public string MessageType { get; init; }

        public string Sender { get; init; }

        public string Message { get; init; }

        [Display(Name = "Заглавието")]
        [Required(ErrorMessage = RequiredFieldMessage)]
        [StringLength(maximumLength: 50, ErrorMessage = LenghtErrorMessage, MinimumLength = 10)]
        public string Title { get; init; }

        [Display(Name = "Саобщението")]
        [Required(ErrorMessage = RequiredFieldMessage)]
        [StringLength(maximumLength: 700, ErrorMessage = LenghtErrorMessage, MinimumLength = 10)]
        public string Text { get; init; }
    }
}
