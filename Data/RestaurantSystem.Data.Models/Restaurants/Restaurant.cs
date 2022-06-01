namespace RestaurantSystem.Data.Models.Restaurants
{
    using System;
    using System.Collections.Generic;

    using RestaurantSystem.Data.Models.Orders;
    using RestaurantSystem.Data.Models.Products;
    using RestaurantSystem.Data.Models.Ratings;
    using RestaurantSystem.Data.Models.Reservations;
    using RestaurantSystem.Data.Models.Users;

    public class Restaurant
    {
        public Restaurant()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Menu = new List<Product>();
            this.Rservations = new List<Reservation>();
            this.Orders = new List<Order>();
            this.Ratings = new List<Rating>();
        }

        public string Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal DeliveryPeice { get; set; }

        public DateTime OpenIn { get; set; }

        public DateTime CloseIn { get; set; }

        public string OwnerId { get; set; }

        public ApplicationUser Owner { get; set; }

        public IEnumerable<Product> Menu { get; set; }

        public IEnumerable<Reservation> Rservations { get; set; }

        public IEnumerable<Order> Orders { get; set; }

        public IEnumerable<Rating> Ratings { get; set; }
    }
}
