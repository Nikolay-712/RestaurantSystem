namespace RestaurantSystem.Services.Restaurants
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RestaurantSystem.Web.ViewModels.Owner.Restaurants;

    public interface IRestaurantService
    {
        Task RegisterRestaurantAsync(string ownerId, RegisterRestaurantInputModel restaurantInputModel);

        Task EditRestaurantAsync(EditRestaurantInputModel restaurantEditModel);

        IEnumerable<T> MyRestaurants<T>(string ownerId);
    }
}
