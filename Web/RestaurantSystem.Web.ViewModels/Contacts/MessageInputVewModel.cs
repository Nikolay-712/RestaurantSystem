namespace RestaurantSystem.Web.ViewModels.Contacts
{
    using System.ComponentModel.DataAnnotations;

    using RestaurantSystem.Data.Models.Contacts;

    using static RestaurantSystem.Common.GlobalConstants;

    public class MessageInputVewModel
    {
        [Required(ErrorMessage = RequiredFieldMessage)]
        [EnumDataType(typeof(MessageType), ErrorMessage = CategoryErrorMesage)]
        public MessageType MessageType { get; init; }

        [Display(Name = "Саобщението")]
        [Required(ErrorMessage = RequiredFieldMessage)]
        [StringLength(maximumLength: 700, ErrorMessage = LenghtErrorMessage, MinimumLength = 10)]
        public string Message { get; init; }
    }
}
