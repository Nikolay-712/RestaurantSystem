namespace RestaurantSystem.Web.ViewModels.Owner.Restaurants
{
    using System.ComponentModel.DataAnnotations;

    using RestaurantSystem.Data.Models.Restaurants;
    using RestaurantSystem.Services.Mapping;

    using static RestaurantSystem.Common.GlobalConstants;

    public class EditRestaurantInputModel : RegisterRestaurantInputModel, IMapFrom<Restaurant>
    {
        [Required(ErrorMessage = RequiredFieldMessage)]
        public string Id { get; init; }
    }
}
