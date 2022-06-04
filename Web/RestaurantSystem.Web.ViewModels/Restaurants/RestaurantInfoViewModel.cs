namespace RestaurantSystem.Web.ViewModels.Restaurants
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using RestaurantSystem.Data.Models.Restaurants;
    using RestaurantSystem.Services.Mapping;
    using RestaurantSystem.Web.ViewModels.Ratings;

    public class RestaurantInfoViewModel : IMapFrom<Restaurant>
    {
        public string Id { get; init; }

        public string Name { get; init; }

        public string Description { get; init; }

        public decimal DeliveryPeice { get; init; }

        public DateTime OpenIn { get; init; }

        public DateTime CloseIn { get; init; }

        public IEnumerable<RatingViewModel> Ratings { get; init; }

        public int RatingsCount => this.Ratings.Count();

        public double AverageRating => Math.Round((double)this.Ratings.Select(x => x.Stars).Sum() / (double)this.RatingsCount, 2);
    }
}
