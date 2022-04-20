namespace RestaurantSystem.Web.Areas.Owner.Controllers.Restaurants
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using RestaurantSystem.Data.Models;
    using RestaurantSystem.Services.Restaurants;
    using RestaurantSystem.Web.ViewModels.Owner.Restaurants;

    public class RestaurantsController : OwnerController
    {
        private readonly IRestaurantService restaurantService;
        private readonly UserManager<ApplicationUser> userManager;

        public RestaurantsController(IRestaurantService restaurantService, UserManager<ApplicationUser> userManager)
        {
            this.restaurantService = restaurantService;
            this.userManager = userManager;
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
                .RegisterRestaurantAsync(this.GetCurrentUserId(), restaurantInputModel);

            return this.RedirectToAction("MyRestaurants");
        }

        public IActionResult MyRestaurants()
        {
            var restaurants = this.restaurantService
                .MyRestaurants<MyRestaurantsViewModel>(this.GetCurrentUserId()).ToList();

            return this.View(restaurants);
        }

        public IActionResult Edit(string restaurantId)
        {
            var restaurant = this.restaurantService
                .MyRestaurants<EditRestaurantInputModel>(this.GetCurrentUserId())
                .FirstOrDefault(x => x.Id == restaurantId);

            if (restaurant == null)
            {
                return this.BadRequest();
            }

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

            return this.RedirectToAction("Stava");
        }

        private string GetCurrentUserId()
        {
            return this.userManager.GetUserId(this.User);
        }
    }
}
