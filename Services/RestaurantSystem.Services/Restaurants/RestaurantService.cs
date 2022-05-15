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
    using RestaurantSystem.Services.Reservations;
    using RestaurantSystem.Web.ViewModels.Owner.Reservations;
    using RestaurantSystem.Web.ViewModels.Owner.Restaurants;

    public class RestaurantService : IRestaurantService
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IReservationService resarvationService;

        public RestaurantService(ApplicationDbContext applicationDbContext, IReservationService resarvationService)
        {
            this.applicationDbContext = applicationDbContext;
            this.resarvationService = resarvationService;
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

        public RestaurantDetailsViewModel Details(string ownerId, string restaurantId)
        {
            var details = this.MyRestaurants<RestaurantDetailsViewModel>(ownerId)
                .FirstOrDefault(x => x.Id == restaurantId);

            if (details == null)
            {
                return details;
            }

            var allReservations = this.resarvationService
                    .AllResarvations<ReservationViewModel>();

            details.Reservations = allReservations
                .Where(x => x.RestaurantId == restaurantId)
                .OrderByDescending(x => x.CreatedOn);

            return details;
        }
    }
}
