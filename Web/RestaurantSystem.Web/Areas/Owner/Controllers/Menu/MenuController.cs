namespace RestaurantSystem.Web.Areas.Owner.Controllers.Menu
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using RestaurantSystem.Services.Menu;
    using RestaurantSystem.Services.Restaurants;
    using RestaurantSystem.Web.Infrastructure;
    using RestaurantSystem.Web.ViewModels.Owner.Menu;

    public class MenuController : OwnerController
    {
        private readonly IMenuService menuService;
        private readonly IRestaurantService restaurantService;

        public MenuController(IMenuService menuService, IRestaurantService restaurantService)
        {
            this.menuService = menuService;
            this.restaurantService = restaurantService;
        }

        public IActionResult AddProduct(string restaurantId)
        {
            if (this.CheckRestaurant(restaurantId))
            {
                return this.View();
            }

            return this.NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            await this.menuService.AddProductAsync(inputModel);

            return this.RedirectToAction("Index", "Menu", new { restaurantId = inputModel.RestaurantId });
        }

        public IActionResult Index(string restaurantId, string category, int page = 1)
        {
            if (this.CheckRestaurant(restaurantId))
            {
                var menu = this.menuService.GetMenu(restaurantId, category, page);
                return this.View(menu);
            }

            return this.NotFound();
        }

        public IActionResult Edit(string productId, string restaurantId, int page)
        {
            if (this.CheckRestaurant(restaurantId))
            {
                var product = this.menuService
                    .GetProducts<EditProductViewModel>(restaurantId)
                    .FirstOrDefault(x => x.Id == productId);

                if (product != null)
                {
                    return this.View(product);
                }

                return this.NotFound();
            }

            return this.NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(bool inStock, string productId, int page, EditProductViewModel editProduct)
        {
            if (this.CheckRestaurant(editProduct.RestaurantId))
            {
                await this.menuService.EditProductAsync(inStock, productId, editProduct);
                return this.RedirectToAction("Index", "Menu", new { restaurantId = editProduct.RestaurantId, page = page });
            }

            return this.NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DailyMenu
            (bool inDailyMenu, string productId, int page, string restaurantId)
        {
            if (this.CheckRestaurant(restaurantId))
            {
                var result = await this.menuService
                     .AddProductToDailyMenuAsync(productId, inDailyMenu);

                if (!result)
                {
                    return this.NotFound();
                }

                return this.RedirectToAction("Index", "Menu", new { restaurantId = restaurantId, page = page });
            }

            return this.NotFound();
        }

        private bool CheckRestaurant(string restaurantId)
        {
            var restaurant = this.restaurantService.GetRestaurant(restaurantId);
            var ownerId = ClaimsPrincipalExtensions.Id(this.User);

            if (restaurant == null) { return false; }

            if (restaurant.OwnerId != ownerId) { return false; }

            return true;
        }
    }
}
