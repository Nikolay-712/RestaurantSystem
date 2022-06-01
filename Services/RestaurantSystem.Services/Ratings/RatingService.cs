namespace RestaurantSystem.Services.Ratings
{
    using System.Linq;
    using System.Threading.Tasks;

    using RestaurantSystem.Data;
    using RestaurantSystem.Data.Models.Ratings;

    public class RatingService : IRatingService
    {
        private readonly ApplicationDbContext applicationDbContext;

        public RatingService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task AddRateAsync(string id, int stars, string userId, string controllerName)
        {
            var existingRating = this.applicationDbContext
                .Ratings
                .FirstOrDefault(x => x.UserId == userId
                    && controllerName == "Restaurants" ? x.RestaurantId == id : x.ProductId == id);

            if (existingRating != null)
            {
                if (existingRating.Stars != stars)
                {
                    existingRating.Stars = stars;
                    this.applicationDbContext.Update(existingRating);
                    await this.applicationDbContext.SaveChangesAsync();
                }
            }
            else
            {
                var rating = new Rating
                {
                    Stars = stars,
                    UserId = userId,
                };

                var currentId = controllerName == "Restaurants" ? rating.RestaurantId = id : rating.ProductId = id;

                await this.applicationDbContext.AddAsync(rating);
                await this.applicationDbContext.SaveChangesAsync();
            }
        }
    }
}
