using RestaurantSystem.Data.Models.Products;
using RestaurantSystem.Services.Mapping;

namespace RestaurantSystem.Web.ViewModels.Owner.Menu
{
    public class EditProductViewModel : ProductInputModel, IMapFrom<Product>
    {
        public string Id { get; set; }

        public string RestaurantId { get; init; }

        public bool InStock { get; set; }
    }
}
