namespace RestaurantSystem.Data.Models.Images
{
    using System;

    public class Image
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string RestaurantId { get; set; }

    }
}
