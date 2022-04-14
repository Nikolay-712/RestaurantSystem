namespace RestaurantSystem.Services.Restaurants
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;

    using RestaurantSystem.Data;
    using RestaurantSystem.Data.Models.Restaurants;
    using RestaurantSystem.Web.ViewModels.Owner.Restaurants;

    public class RestaurantService : IRestaurantService
    {
        private readonly ApplicationDbContext applicationDbContext;

        public RestaurantService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task RegisterRestaurantAsync(string ownerId, RegisterRestaurantInputModel restaurantInputModel)
        {
            var restaurant = new Restaurant
            {
                Name = restaurantInputModel.Name,
                Description = restaurantInputModel.Description,
                DeliveryPeice = restaurantInputModel.DeliveryPeice,
                OpenIn = DateTime.Parse(restaurantInputModel.OpenIn, CultureInfo.InvariantCulture),
                CloseIn = DateTime.Parse(restaurantInputModel.CloseIn, CultureInfo.InvariantCulture),
                OwnerId = ownerId,
            };

            await this.applicationDbContext.Restaurants.AddAsync(restaurant);
            await this.applicationDbContext.SaveChangesAsync();
        }

        public bool ExistingRestaurantName(string restaurantName)
        {
            return this.applicationDbContext
                .Restaurants
                .Select(x => x.Name)
                .Contains(restaurantName);
        }
    }
}
