namespace RestaurantSystem.Web.ViewModels.Menu
{
    using System.Collections.Generic;

    using RestaurantSystem.Data.Models.Restaurants;
    using RestaurantSystem.Services.Mapping;
    using RestaurantSystem.Web.ViewModels.Orders;

    public class MenuViewModel : IMapFrom<Restaurant>
    {
        public string Id { get; set; }

        public IEnumerable<ProductViewModel> Menu { get; set; }

        public OrderViewModel Order { get; set; }
    }
}
