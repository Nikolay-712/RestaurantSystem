namespace RestaurantSystem.Web.ViewModels.Administration.Messages
{
    using System;
    using System.Collections.Generic;
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

        public bool IsRead { get; init; }

        public AdminMessageViewModel Answer { get; set; }

        [Display(Name = "Саобщението")]
        [Required(ErrorMessage = RequiredFieldMessage)]
        [StringLength(maximumLength: 700, ErrorMessage = LenghtErrorMessage, MinimumLength = 10)]
        public string Text { get; init; }
    }
}
