namespace RestaurantSystem.Web.ViewModels.Owner.Menu
{
    using RestaurantSystem.Data.Models.Products;
    using RestaurantSystem.Services.Mapping;

    public class EditProductViewModel : ProductInputModel, IMapFrom<Product>
    {
        public string Id { get; set; }

        public bool InStock { get; set; }

        public int Page { get; set; }
    }
}
