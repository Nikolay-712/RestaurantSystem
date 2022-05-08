using RestaurantSystem.Data.Models.Contacts;
using RestaurantSystem.Services.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RestaurantSystem.Common.GlobalConstants;

namespace RestaurantSystem.Web.ViewModels.Administration.Messages
{
    public class ReadMessageViewModel : IMapFrom<AppMessage>
    {
        public string Id { get; set; }

        public string Message { get; init; }

        public string MessageType { get; set; }

        public IEnumerable<ReplieViewModel> Replies { get; set; }

        [Display(Name = "Саобщението")]
        [Required(ErrorMessage = RequiredFieldMessage)]
        [StringLength(maximumLength: 700, ErrorMessage = LenghtErrorMessage, MinimumLength = 10)]
        public string Text { get; init; }
    }
}
