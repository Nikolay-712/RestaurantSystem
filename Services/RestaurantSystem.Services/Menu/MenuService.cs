namespace RestaurantSystem.Services.Menu
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using RestaurantSystem.Data;
    using RestaurantSystem.Data.Models.Products;
    using RestaurantSystem.Services.Mapping;
    using RestaurantSystem.Web.ViewModels.Owner.Menu;

    public class MenuService : IMenuService
    {
        private const int ProductsPerpage = 15;
        private readonly ApplicationDbContext applicationDbContext;

        public MenuService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task AddProductAsync(ProductInputModel inputModel)
        {
            var product = new Product
            {
                Name = inputModel.Name,
                Category = inputModel.Category,
                Description = inputModel.Description,
                Price = inputModel.Price,
                Weight = inputModel.Weight,
                InStock = true,
                RestaurantId = inputModel.RestaurantId,
            };

            await this.applicationDbContext.Products.AddAsync(product);
            await this.applicationDbContext.SaveChangesAsync();
        }

        public IEnumerable<T> GetProducts<T>(string restaurantId)
        {
            var menu = this.applicationDbContext
                .Products
                .Where(x => x.RestaurantId == restaurantId)
                .To<T>();

            return menu;
        }

        public MenuViewModel GetMenu(string restaurantId, string category, int page)
        {
            var products = this.GetProducts<ProductViewModel>(restaurantId);

            if (category != null)
            {
                products = products.Where(x => x.Category == category);
            }

            var menu = new MenuViewModel
            {
                RestaurantId = restaurantId,
                ItemsPerPage = ProductsPerpage,
                ItemsCount = products.Count(),
                PageNumber = page,
                Products = products
                    .OrderBy(x => x.Category)
                    .Skip((page - 1) * ProductsPerpage)
                    .Take(ProductsPerpage).ToList(),
            };

            return menu;
        }

        public RestaurantSystem.Web.ViewModels.Menu.MenuViewModel ShowRestaurantMenu
            (string restaurantId, string category, string userId)
        {
            var menu = this.applicationDbContext
                .Restaurants
                .To<RestaurantSystem.Web.ViewModels.Menu.MenuViewModel>()
                .FirstOrDefault(x => x.Id == restaurantId);

            if (menu == null)
            {
                return menu;
            }

            var categories = menu.Menu.Where(x => x.InStock).Select(x => x.Category);
            var products = category != null ? menu.Menu.Where(x => x.Category == category) : menu.Menu;

            if (category == "MostRated")
            {
                 products = menu
                    .Menu
                    .OrderByDescending(x => x.AverageRating)
                    .ToList().GetRange(0, 5);
            }

            menu.Categories = categories.ToHashSet<string>();
            menu.Menu = products.Where(x => x.InStock);
            menu.Category = category;

            return menu;
        }

        public void AddProductToDalyMenuAsync()
        {

        }

        public async Task EditProductAsync(bool inStock, string productId, EditProductViewModel editProduct)
        {
            if (editProduct.Id == null)
            {
                editProduct.Id = productId;
                editProduct.InStock = inStock;
            }

            var product = this.applicationDbContext
                .Products
                .FirstOrDefault(x => x.Id == editProduct.Id);

            if (productId != null)
            {
                product.InStock = editProduct.InStock ? false : true;
            }
            else
            {
                product.Name = editProduct.Name;
                product.Description = editProduct.Description;
                product.Price = editProduct.Price;
                product.Weight = editProduct.Weight;
            }

            this.applicationDbContext.Update(product);
            await this.applicationDbContext.SaveChangesAsync();
        }
    }
}
