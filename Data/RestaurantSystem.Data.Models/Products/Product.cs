namespace RestaurantSystem.Data.Models.Products
{
    using System;
    using System.Collections.Generic;

    using RestaurantSystem.Data.Models.Orders;
    using RestaurantSystem.Data.Models.Ratings;

    public class Product
    {
        public Product()
        {
            this.Id = Guid.NewGuid().ToString();
            this.OrderProducts = new List<OrderProducts>();
            this.Ratings = new List<Rating>();
        }

        public string Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Name { get; set; }

        public Category Category { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Weight { get; set; }

        public bool InStock { get; set; }

        public bool InDalyMenu { get; set; }

        public string RestaurantId { get; set; }

        public ICollection<OrderProducts> OrderProducts { get; set; }

        public ICollection<Rating> Ratings { get; set; }
    }
}
