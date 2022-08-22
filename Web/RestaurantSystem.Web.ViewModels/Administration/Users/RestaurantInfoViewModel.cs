namespace RestaurantSystem.Web.ViewModels.Administration.Users
{
    using System;

    using RestaurantSystem.Data.Models.Restaurants;
    using RestaurantSystem.Services.Mapping;

    public class RestaurantInfoViewModel : IMapFrom<Restaurant>
    {
        public string Id { get; init; }

        public DateTime CreatedOn { get; init; }

        public string Name { get; init; }
    }
}
