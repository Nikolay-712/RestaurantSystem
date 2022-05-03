namespace RestaurantSystem.Web.Areas.Owner.Controllers.Restaurants
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using RestaurantSystem.Data.Models;
    using RestaurantSystem.Services.Reservations;
    using RestaurantSystem.Services.Restaurants;
    using RestaurantSystem.Web.Infrastructure;
    using RestaurantSystem.Web.ViewModels.Owner.Reservations;
    using RestaurantSystem.Web.ViewModels.Owner.Restaurants;

    public class RestaurantsController : OwnerController
    {
        private readonly IRestaurantService restaurantService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IReservationService reservationService;

        public RestaurantsController(
            IRestaurantService restaurantService,
            UserManager<ApplicationUser> userManager,
            IReservationService reservationService)
        {
            this.restaurantService = restaurantService;
            this.userManager = userManager;
            this.reservationService = reservationService;
        }

        public IActionResult Registration()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegisterRestaurantInputModel restaurantInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(restaurantInputModel);
            }

            await this.restaurantService
                .RegisterRestaurantAsync(ClaimsPrincipalExtensions.Id(this.User), restaurantInputModel);

            return this.RedirectToAction("MyRestaurants");
        }

        public IActionResult MyRestaurants()
        {
            var restaurants = this.restaurantService
                .MyRestaurants<MyRestaurantsViewModel>(ClaimsPrincipalExtensions.Id(this.User)).ToList();

            return this.View(restaurants);
        }

        public IActionResult Edit(string restaurantId)
        {
            var restaurant = this.restaurantService
                .MyRestaurants<EditRestaurantInputModel>(ClaimsPrincipalExtensions.Id(this.User))
                .FirstOrDefault(x => x.Id == restaurantId);

            if (restaurant == null)
            {
                return this.NotFound();
            }

            var timeOpenIn = restaurant.OpenIn.Split(new[] { " ", ":" }, StringSplitOptions.RemoveEmptyEntries);
            var timeCloseIn = restaurant.CloseIn.Split(new[] { " ", ":" }, StringSplitOptions.RemoveEmptyEntries);

            restaurant.OpenIn = $"{timeOpenIn[1]}:{timeOpenIn[2]}";
            restaurant.CloseIn = $"{timeCloseIn[1]}:{timeCloseIn[2]}";

            return this.View(restaurant);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditRestaurantInputModel restaurantEditModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(restaurantEditModel);
            }

            await this.restaurantService.EditRestaurantAsync(restaurantEditModel);

            return this.RedirectToAction("MyRestaurants");
        }

        public async Task<IActionResult> ChangeStatus(ReservationViewModel reservation)
        {
            var result = await this.reservationService
                .ChangeReservationStatusAsync(reservation.Id, reservation.ReservationStatus);

            if (!result)
            {
                return this.NotFound();
            }

            return this.RedirectToAction("Details", new { restaurantId = reservation.RestaurantId });
        }

        public IActionResult Details(string restaurantId)
        {
            var details = this.restaurantService
                .Details(ClaimsPrincipalExtensions.Id(this.User), restaurantId);

            if (details == null)
            {
                return this.NotFound();
            }

            return this.View(details);
        }
    }
}
