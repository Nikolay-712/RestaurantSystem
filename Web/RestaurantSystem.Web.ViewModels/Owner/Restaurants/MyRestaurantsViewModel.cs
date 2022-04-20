namespace RestaurantSystem.Web.ViewModels.Owner.Restaurants
{
    using System;

    using RestaurantSystem.Data.Models.Restaurants;
    using RestaurantSystem.Services.Mapping;

    public class MyRestaurantsViewModel : IMapFrom<Restaurant>
    {
        public DateTime CreatedOn { get; set; }

        public string Id { get; init; }

        public string Name { get; init; }

        public string Description { get; init; }

        public decimal DeliveryPeice { get; init; }

        public DateTime OpenIn { get; init; }

        public DateTime CloseIn { get; init; }
    }
}
