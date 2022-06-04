namespace RestaurantSystem.Services.Ratings
{
    using System.Threading.Tasks;

    using RestaurantSystem.Web.ViewModels.Ratings;

    public interface IRatingService
    {
        Task AddRateAsync(RatingInputModel ratingInput, string userId);
    }
}
