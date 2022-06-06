namespace RestaurantSystem.Web.ViewModels.Owner.Menu
{
    using RestaurantSystem.Data.Models.Products;
    using RestaurantSystem.Services.Mapping;

    public class ProductViewModel : IMapFrom<Product>
    {
        public string Id { get; set; }

        public string Name { get; init; }

        public string Category { get; init; }

        public string Description { get; init; }

        public decimal Price { get; init; }

        public int Weight { get; init; }

        public string RestaurantId { get; init; }

        public bool InStock { get; init; }

        public bool InDalyMenu { get; init; }
    }
}
