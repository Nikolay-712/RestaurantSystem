namespace RestaurantSystem.Web.ViewModels.Restaurants
{
    using System;
    using System.Collections.Generic;

    using RestaurantSystem.Data.Models.Restaurants;
    using RestaurantSystem.Services.Mapping;
    using RestaurantSystem.Web.ViewModels.Menu;

    public class RestaurantInfoViewModel : IMapFrom<Restaurant>
    {
        public string Id { get; init; }

        public string Name { get; init; }

        public string Description { get; init; }

        public decimal DeliveryPeice { get; init; }

        public DateTime OpenIn { get; init; }

        public DateTime CloseIn { get; init; }
    }
}
