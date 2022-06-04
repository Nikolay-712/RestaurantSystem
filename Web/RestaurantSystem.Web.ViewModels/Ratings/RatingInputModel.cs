namespace RestaurantSystem.Web.ViewModels.Ratings
{
    using System.ComponentModel.DataAnnotations;

    public class RatingInputModel
    {
        [Required]
        public string ObjectId { get; init; }

        [Required]
        [Range(minimum: 1, maximum: 5)]
        public int Rating { get; init; }

        [Required]
        public string ObjectType { get; init; }

        public string RestaurantId { get; init; }
    }
}
