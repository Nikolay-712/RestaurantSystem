namespace RestaurantSystem.Services.Restaurants
{
    using System.Threading.Tasks;

    using RestaurantSystem.Web.ViewModels.Owner.Restaurants;

    public interface IRestaurantService
    {
        Task RegisterRestaurantAsync(string ownerId, RegisterRestaurantInputModel restaurantInputModel);

        bool ExistingRestaurantName(string restaurantName);
    }
}
