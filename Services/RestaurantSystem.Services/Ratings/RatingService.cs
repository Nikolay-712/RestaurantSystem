namespace RestaurantSystem.Services.Ratings
{
    using System.Linq;
    using System.Threading.Tasks;

    using RestaurantSystem.Data;
    using RestaurantSystem.Data.Models.Ratings;
    using RestaurantSystem.Web.ViewModels.Ratings;

    public class RatingService : IRatingService
    {
        private readonly ApplicationDbContext applicationDbContext;

        public RatingService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task AddRateAsync(RatingInputModel ratingInput, string userId)
        {
            var existingRating = this.applicationDbContext
                .Ratings
                .Where(x => x.UserId == userId).ToList()
                .Where(x => x.ProductId == null ?
                    x.RestaurantId == ratingInput.ObjectId :
                    x.ProductId == ratingInput.ObjectId)
                .FirstOrDefault();

            if (existingRating != null)
            {
                if (existingRating.Stars != ratingInput.Rating)
                {
                    existingRating.Stars = ratingInput.Rating;
                    this.applicationDbContext.Update(existingRating);
                    await this.applicationDbContext.SaveChangesAsync();
                }
            }
            else
            {
                var rating = new Rating
                {
                    Stars = ratingInput.Rating,
                    UserId = userId,
                };

                var currentId = ratingInput.ObjectType
                    .Contains("Restaurant") ? rating.RestaurantId = ratingInput.ObjectId : rating.ProductId = ratingInput.ObjectId;

                await this.applicationDbContext.Ratings.AddAsync(rating);
                await this.applicationDbContext.SaveChangesAsync();
            }
        }
    }
}
