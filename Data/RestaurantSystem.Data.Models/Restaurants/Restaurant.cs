﻿namespace RestaurantSystem.Data.Models.Restaurants
{
    using System;
    using System.Collections.Generic;

    using RestaurantSystem.Data.Models.Products;

    public class Restaurant
    {
        public Restaurant()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Menu = new List<Product>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal DeliveryPeice { get; set; }

        public DateTime OpenIn { get; set; }

        public DateTime CloseIn { get; set; }

        public string OwnerId { get; set; }

        public ApplicationUser Owner { get; set; }

        public IEnumerable<Product> Menu { get; set; }
    }
}
