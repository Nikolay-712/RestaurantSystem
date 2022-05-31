namespace RestaurantSystem.Web.ViewModels.Menu
{
    using System.Collections.Generic;
    using System.Linq;
    using RestaurantSystem.Data.Models.Restaurants;
    using RestaurantSystem.Services.Mapping;
    using RestaurantSystem.Web.ViewModels.Orders;

    public class MenuViewModel : IMapFrom<Restaurant>
    {
        public string Id { get; init; }

        public IEnumerable<ProductViewModel> Menu { get; set; }

        public OrderViewModel Order { get; set; }

        public decimal DeliveryPeice { get; init; }

        public IEnumerable<string> Categories { get; set; }

        public string Category { get; set; }

    }
}
