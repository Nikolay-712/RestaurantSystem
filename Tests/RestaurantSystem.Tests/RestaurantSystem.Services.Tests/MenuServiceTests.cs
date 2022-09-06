using RestaurantSystem.Data;
using RestaurantSystem.Services.Menu;
using RestaurantSystem.Web.ViewModels.Owner.Menu;
using RestaurantSystem.Data.Models.Products;
using RestaurantSystem.Data.Models.Restaurants;

using Microsoft.EntityFrameworkCore;
using Xunit;

namespace RestaurantSystem.Tests.RestaurantSystem.Services.Tests
{
    public class MenuServiceTests
    {
        private ApplicationDbContext dbContext;
        private MenuService menuService;
        
        public MenuServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ApplicationTestDb").Options;

            this.dbContext = new ApplicationDbContext(options);
            this.menuService = new MenuService(this.dbContext);

            if (this.dbContext.Restaurants.Count() == 0)
            {
                var restaurantId = this.AddTestRestaurant();
                this.AddProductsToDbContext(restaurantId);
            }
        }

        [Fact]
        public async Task AddProductWithValidInputModel()
        {
            var restaurantId = this.GetRestaUrantId();
            await this.menuService.AddProductAsync(new ProductInputModel
            {
                Name = "TestProduct2",
                Category = Category.Супа,
                Description = "description for test products",
                Price = 15.20m,
                Weight = 250,
                RestaurantId = restaurantId,
            });

            var restaurant = this.dbContext.Restaurants.FirstOrDefault();
            var product = this.dbContext.Products.FirstOrDefault();

            Assert.Equal(5, this.dbContext.Products.Count());
            Assert.Equal(product.RestaurantId, restaurantId);
        }

        [Theory]
        [InlineData("test products", Category.Десерт, "test", 10, 200, null)]
        [InlineData(null, Category.Десерт, "test", 10, 200, "testId")]
        [InlineData("product", Category.Пица, null, 15, 200, "testId")]
        public async Task AddProductWithInValidInputModel
            (string name, Category category, string description, decimal price, int weight, string restaurantId)
        {
            await Assert.ThrowsAsync<DbUpdateException>(()
                 => this.menuService.AddProductAsync(new ProductInputModel
                 {
                     Name = name,
                     Category = category,
                     Description = description,
                     Price = price,
                     Weight = weight,
                     RestaurantId = restaurantId,
                 }));
        }

        [Fact]
        public void SetProductInformationCorrectly()
        {
            var products = this.dbContext.Products;
            var product = this.dbContext.Products.FirstOrDefault(x => x.Name == "TestProduct1");

            Assert.NotNull(product);
            Assert.Equal(10.46m, product.Price);
            Assert.Equal(200, product.Weight);
            Assert.Equal(Category.Салата.ToString(), product.Category.ToString());
            Assert.True(product.InStock);

        }

        [Fact]
        public async Task EditProductDetailsShouldBySuccessfully()
        {
            var product = this.dbContext.Products
                .FirstOrDefault(x => x.Name == "TestProduct");

            var editModel = new EditProductViewModel
            {
                Id = product.Id,
                Name = "new product name",
                Description = "new description",
                Price = 15,
                Weight = 234,
            };

            await this.menuService.EditProductAsync(true, null, editModel);

            Assert.NotNull(product);
            Assert.Equal("new product name", product.Name);
            Assert.Equal("new description", product.Description);
            Assert.Equal(15, product.Price);
            Assert.Equal(234, product.Weight);

        }

        [Fact]
        public async Task EditProductChangeIsItAvailableInMenu()
        {
            var product = this.dbContext.Products
                .FirstOrDefault(x => x.Name == "new product name");

            await this.menuService.EditProductAsync
                (product.InStock, product.Id, new EditProductViewModel
                {
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Weight = product.Weight,
                });

            Assert.False(product.InStock);
        }

        [Fact]
        public async Task AddProductToToDailyMenuShouldReturnTrue()
        {
            var product = this.dbContext.Products.FirstOrDefault();
            var restaurant = this.dbContext.Restaurants.FirstOrDefault();

            var result = await this.menuService
                 .AddProductToDailyMenuAsync(product.Id, product.InDalyMenu);

            Assert.True(result);
            Assert.True(product.InDalyMenu);

            var menu = restaurant.Menu.Where(x => x.InDalyMenu);

            Assert.Equal(1, menu.Count());
        }

        [Fact]
        public async Task RemoveProductFromToDailyMenuShouldReturnTrue()
        {
            var product = this.dbContext.Products.FirstOrDefault(x => x.Name == "TestProduct5");
            product.InDalyMenu = true;

            var restaurant = this.dbContext.Restaurants.FirstOrDefault();

            var result = await this.menuService
                 .AddProductToDailyMenuAsync(product.Id, product.InDalyMenu);

            Assert.True(result);
            Assert.False(product.InDalyMenu);

            var menu = restaurant.Menu.Where(x => x.InDalyMenu);

            Assert.Equal(0, menu.Count());

        }

        private string AddTestRestaurant()
        {
            var restaurant = new Restaurant
            {
                CreatedOn = DateTime.Now,
                Name = "Test Restaurant",
                Description = "description for test restaurant",
                DeliveryPeice = 1m,
                OpenIn = DateTime.ParseExact("10:30", "HH:mm", null),
                CloseIn = DateTime.ParseExact("22:30", "HH:mm", null),
                OwnerId = "testOwnerId",
            };

            this.dbContext.Restaurants.Add(restaurant);
            this.dbContext.SaveChanges();

            return restaurant.Id;
        }

        private string GetRestaUrantId()
        {
            return this.dbContext.Restaurants.FirstOrDefault().Id;
        }

        private void AddProductsToDbContext(string restaurantId)
        {
            var products = new List<Product>
            {
                 new Product
                 {
                     Name = "TestProduct",
                     Category = Category.Основно,
                     Description = "description for test products",
                     Price = 12.46m,
                     Weight = 300,
                     RestaurantId = restaurantId,
                     InStock = true,
                 },

                 new Product
                 {
                     Name = "TestProduct1",
                     Category = Category.Салата,
                     Description = "description for test products",
                     Price = 10.46m,
                     Weight = 200,
                     RestaurantId = restaurantId,
                     InStock = true,
                 },

                 new Product
                 {
                     Name = "TestProduct2",
                     Category = Category.Десерт,
                     Description = "description for test products",
                     Price = 7.46m,
                     Weight = 400,
                     RestaurantId = restaurantId,
                     InStock = true,
                 },

                 new Product
                 {
                     Name = "TestProduct5",
                     Category = Category.Бургери,
                     Description = "description for test products",
                     Price = 10m,
                     Weight = 250,
                     RestaurantId = restaurantId,
                     InStock = true,
                 },
            };

            this.dbContext.Products.AddRange(products);
            this.dbContext.SaveChanges();
        }
    }
}
