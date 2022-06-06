namespace RestaurantSystem.Services.Menu
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RestaurantSystem.Web.ViewModels.Owner.Menu;

    public interface IMenuService
    {
        Task AddProductAsync(ProductInputModel inputModel);

        IEnumerable<T> GetProducts<T>(string restaurantId);

        MenuViewModel GetMenu(string restaurantId, string category, int page);

        RestaurantSystem.Web.ViewModels.Menu.MenuViewModel ShowRestaurantMenu(string restaurantId, string category, string userId);

        Task<bool> AddProductToDailyMenuAsync(string productId, bool inDalyMenu);

        Task EditProductAsync(bool inStock, string productId, EditProductViewModel editProduct);
    }
}
