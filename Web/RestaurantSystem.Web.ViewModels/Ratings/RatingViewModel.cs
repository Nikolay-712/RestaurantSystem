namespace RestaurantSystem.Web.ViewModels.Ratings
{
    using RestaurantSystem.Data.Models.Ratings;
    using RestaurantSystem.Services.Mapping;

    public class RatingViewModel : IMapFrom<Rating>
    {
        public string Id { get; set; }

        public int Stars { get; set; }

        public string RestaurantId { get; set; }

        public string ProductId { get; set; }

        public string UserId { get; set; }

        public RatingInputModel RatingInput { get; set; }

    }
}
