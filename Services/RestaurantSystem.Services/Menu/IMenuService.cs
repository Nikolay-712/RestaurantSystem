namespace RestaurantSystem.Services.Menu
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RestaurantSystem.Web.ViewModels.Owner.Menu;

    public interface IMenuService
    {
        Task AddProductAsync(ProductInputModel inputModel);

        IEnumerable<T> GetRestaurantMenu<T>(string restaurantId);

        Task EditProductAsync(bool inStock, string productId, EditProductViewModel editProduct);
    }
}
