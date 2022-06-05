namespace RestaurantSystem.Web.ViewModels.Menu
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using RestaurantSystem.Data.Models.Products;
    using RestaurantSystem.Services.Mapping;
    using RestaurantSystem.Web.ViewModels.Ratings;

    public class ProductViewModel : IMapFrom<Product>
    {
        public string Id { get; init; }

        public string Name { get; init; }

        public string Category { get; init; }

        public string Description { get; init; }

        public decimal Price { get; init; }

        public int Weight { get; init; }

        public bool InStock { get; set; }

        public IEnumerable<RatingViewModel> Ratings { get; init; }

        public int RatingsCount => this.Ratings.Count();

        public double AverageRating => Math.Round((double)this.Ratings.Select(x => x.Stars).Sum() / (double)this.RatingsCount, 2);
    }
}
