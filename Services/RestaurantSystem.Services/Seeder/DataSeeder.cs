namespace RestaurantSystem.Services.Seeder
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;

    using RestaurantSystem.Data;
    using RestaurantSystem.Data.Models.Products;
    using RestaurantSystem.Data.Models.Restaurants;

    public class DataSeeder : IDataSeeder
    {
        private readonly ApplicationDbContext applicationDbContext;

        public DataSeeder(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task AddRestaurantAsync()
        {
            var user = this.applicationDbContext
                .Users
                .FirstOrDefault(x => x.Email == "owner@abv.bg");

            var restaurant = new Restaurant
            {
                CreatedOn = DateTime.UtcNow,
                Name = "Test Restaurant",
                Description = "Най доброто място за вашите поръчки на вкусна храна",
                DeliveryPeice = 2.5m,
                OpenIn = DateTime.Parse("10:30", CultureInfo.InvariantCulture),
                CloseIn = DateTime.Parse("22:30", CultureInfo.InvariantCulture),
                OwnerId = user.Id,
            };

            await this.applicationDbContext.Restaurants.AddAsync(restaurant);
            await this.applicationDbContext.SaveChangesAsync();

            await this.AddProductsAsync(restaurant.Id);
        }

        public bool CheckDataBase()
        {
            return this.applicationDbContext
                .Restaurants.Any(x => x.Name == "Test Restaurant");
        }

        private async Task AddProductsAsync(string restaurantId)
        {
            for (int i = 0; i < 25; i++)
            {
                var product = new Product
                {
                    Name = $"Продукт - {i}",
                    Category = this.GetRandomCategory(),
                    Description = "това е тестови продукт, моля оценте продукта.",
                    Price = this.SetRandomPrice(),
                    Weight = 200,
                    InStock = true,
                    RestaurantId = restaurantId,
                };

                await this.applicationDbContext.Products.AddAsync(product);
            }

            await this.applicationDbContext.SaveChangesAsync();
        }

        private Category GetRandomCategory()
        {
            var values = Enum.GetValues(typeof(Category));
            Random random = new Random();

            Category category = (Category)values.GetValue(random.Next(values.Length));
            return category;
        }

        private decimal SetRandomPrice()
        {
            Random random = new Random();

            var price = random.Next(1, 100);
            return (decimal)price;
        }
    }
}
