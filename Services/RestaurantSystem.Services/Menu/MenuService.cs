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

        public IEnumerable<T> GetRestaurantMenu<T>(string restaurantId)
        {
            var menu = this.applicationDbContext
                .Products
                .Where(x => x.RestaurantId == restaurantId)
                .To<T>()
                .ToList();

            return menu;
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
