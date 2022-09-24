namespace RestaurantSystem.Web.ViewModels.Restaurants
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using RestaurantSystem.Data.Models.Restaurants;
    using RestaurantSystem.Services.Mapping;
    using RestaurantSystem.Web.ViewModels.Ratings;

    using static RestaurantSystem.Common.GlobalConstants;

    public class RestaurantInfoViewModel : IMapFrom<Restaurant>
    {
        public string Id { get; init; }

        public string Name { get; init; }

        public string Description { get; init; }

        public decimal DeliveryPeice { get; init; }

        public DateTime OpenIn { get; init; }

        public DateTime CloseIn { get; init; }

        public string CoverImageUrl { get; init; }

        public IEnumerable<RatingViewModel> Ratings { get; init; }

        public IEnumerable<CommentViewModel> Comments { get; init; }

        public int RatingsCount => this.Ratings.Count();

        [Display(Name = "Коментара")]
        [Required(ErrorMessage = RequiredFieldMessage)]
        [StringLength(maximumLength: 300, ErrorMessage = LenghtErrorMessage, MinimumLength = 10)]
        public string Comment { get; init; }

        public double AverageRating
        {
            get =>
                this.RatingsCount == 0 ? 0 : Math.Round((double)this.Ratings.Select(x => x.Stars).Average(), 2);
        }
    }
}
