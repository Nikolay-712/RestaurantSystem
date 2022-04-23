namespace RestaurantSystem.Web.Areas.Owner.Controllers.Menu
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using RestaurantSystem.Services.Menu;
    using RestaurantSystem.Web.ViewModels.Owner.Menu;

    public class MenuController : OwnerController
    {
        private readonly IMenuService menuService;

        public MenuController(IMenuService menuService)
        {
            this.menuService = menuService;
        }

        public IActionResult Edit(string productId, string restaurantId)
        {
            var product = this.menuService
                .GetRestaurantMenu<EditProductViewModel>(restaurantId)
                .FirstOrDefault(x => x.Id == productId);

            return this.View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(bool inStock, string productId, EditProductViewModel editProduct)
        {
            await this.menuService.EditProductAsync(inStock, productId, editProduct);
            return this.RedirectToAction("Index", "Menu", new { restaurantId = editProduct.RestaurantId });
        }

        public IActionResult Index(string restaurantId, string category)
        {
            var products = category is null ?
                this.menuService.GetRestaurantMenu<ProductViewModel>(restaurantId) :
                this.menuService.GetRestaurantMenu<ProductViewModel>(restaurantId).Where(x => x.Category == category);

            var menu = new MenuViewModel
            {
                Products = products,
                RestaurantId = restaurantId,
            };

            return this.View(menu);
        }

        public IActionResult AddProduct(string restaurantId)
        {
            return this.View();
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
    }
}
