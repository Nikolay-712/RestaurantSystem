namespace RestaurantSystem.Services.Restaurants
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;

    using RestaurantSystem.Data;
    using RestaurantSystem.Data.Models.Restaurants;
    using RestaurantSystem.Services.Mapping;
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

        public async Task EditRestaurantAsync(EditRestaurantInputModel restaurantEditModel)
        {
            var restaurant = this.applicationDbContext
                .Restaurants
                .FirstOrDefault(x => x.Id == restaurantEditModel.Id);

            restaurant.Name = restaurantEditModel.Name;
            restaurant.Description = restaurant.Description;
            restaurant.DeliveryPeice = restaurantEditModel.DeliveryPeice;
            restaurant.OpenIn = DateTime.Parse(restaurantEditModel.OpenIn, CultureInfo.InvariantCulture);
            restaurant.CloseIn = DateTime.Parse(restaurantEditModel.CloseIn, CultureInfo.InvariantCulture);

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
    }
}
