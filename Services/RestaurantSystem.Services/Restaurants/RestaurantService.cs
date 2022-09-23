namespace RestaurantSystem.Services.Restaurants
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;

    using RestaurantSystem.Data;
    using RestaurantSystem.Data.Models.Comments;
    using RestaurantSystem.Data.Models.Restaurants;
    using RestaurantSystem.Services.Images;
    using RestaurantSystem.Services.Mapping;
    using RestaurantSystem.Web.ViewModels.Owner.Restaurants;

    public class RestaurantService : IRestaurantService
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IImageService imageService;

        public RestaurantService(ApplicationDbContext applicationDbContext, IImageService imageService)
        {
            this.applicationDbContext = applicationDbContext;
            this.imageService = imageService;
        }

        public async Task RegisterRestaurantAsync(string ownerId, RegisterRestaurantInputModel restaurantInputModel)
        {
            var restaurant = new Restaurant
            {
                CreatedOn = DateTime.UtcNow,
                Name = restaurantInputModel.Name,
                Description = restaurantInputModel.Description,
                DeliveryPeice = restaurantInputModel.DeliveryPeice,
                OpenIn = DateTime.Parse(restaurantInputModel.OpenIn, CultureInfo.InvariantCulture),
                CloseIn = DateTime.Parse(restaurantInputModel.CloseIn, CultureInfo.InvariantCulture),
                OwnerId = ownerId,
            };

            if (restaurantInputModel.CoverImage != null)
            {
                var imageUrl = await this.imageService
                    .UploadFileAsync(restaurant.Id, restaurantInputModel.CoverImage);

                restaurant.CoverImageUrl = imageUrl;
            }

            await this.applicationDbContext.Restaurants.AddAsync(restaurant);
            await this.applicationDbContext.SaveChangesAsync();
        }

        public async Task EditRestaurantAsync(EditRestaurantInputModel restaurantEditModel)
        {
            var restaurant = this.GetRestaurant(restaurantEditModel.Id);

            restaurant.Name = restaurantEditModel.Name;
            restaurant.Description = restaurantEditModel.Description;
            restaurant.DeliveryPeice = restaurantEditModel.DeliveryPeice;
            restaurant.OpenIn = DateTime.Parse(restaurantEditModel.OpenIn, CultureInfo.InvariantCulture);
            restaurant.CloseIn = DateTime.Parse(restaurantEditModel.CloseIn, CultureInfo.InvariantCulture);

            if (restaurantEditModel.CoverImage != null)
            {
                var imageUrl = await this.imageService
                    .UploadFileAsync(restaurant.Id, restaurantEditModel.CoverImage);

                restaurant.CoverImageUrl = imageUrl;
            }

            this.applicationDbContext.Update(restaurant);
            await this.applicationDbContext.SaveChangesAsync();
        }

        public IEnumerable<T> MyRestaurants<T>(string ownerId)
        {
            return this.applicationDbContext
                .Restaurants
                .Where(x => x.OwnerId == ownerId)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> AllRestaurants<T>()
        {
            return this.applicationDbContext
                .Restaurants
                .To<T>()
                .ToList();
        }

        public Restaurant GetRestaurant(string restaurantid)
        {
            return this.applicationDbContext
                .Restaurants
                .FirstOrDefault(x => x.Id == restaurantid);
        }

        public async Task AddCommentAsync(string restaurantId, string userId, string commentText)
        {
            var comment = new Comment
            {
                CreatedOn = DateTime.Now,
                RestaurantId = restaurantId,
                UserId = userId,
                Text = commentText,
            };

            await this.applicationDbContext.Comments.AddAsync(comment);
            await this.applicationDbContext.SaveChangesAsync();
        }
    }
}
