namespace RestaurantSystem.Services.Restaurants
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RestaurantSystem.Data.Models.Restaurants;
    using RestaurantSystem.Web.ViewModels.Owner.Restaurants;

    public interface IRestaurantService
    {
        Task RegisterRestaurantAsync(string ownerId, RegisterRestaurantInputModel restaurantInputModel);

        Task EditRestaurantAsync(EditRestaurantInputModel restaurantEditModel);

        IEnumerable<T> MyRestaurants<T>(string ownerId);

        IEnumerable<T> AllRestaurants<T>();

        Restaurant GetRestaurant(string restaurantid);
    }
}
