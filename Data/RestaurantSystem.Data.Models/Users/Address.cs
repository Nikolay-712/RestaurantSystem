namespace RestaurantSystem.Data.Models.Users
{
    using System;

    public class Address
    {
        public Address()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string Country { get; set; }

        public string Town { get; set; }

        public string ShippingAddress { get; set; }

        public string UseId { get; set; }
    }
}
