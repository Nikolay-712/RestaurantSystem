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

        Task EditProductAsync(bool inStock, string productId, EditProductViewModel editProduct);
    }
}
