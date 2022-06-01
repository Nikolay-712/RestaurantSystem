namespace RestaurantSystem.Data.Models.Ratings
{
    using System;

    public class Rating
    {
        public Rating()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public int Stars { get; set; }

        public string RestaurantId { get; set; }

        public string ProductId { get; set; }

        public string UserId { get; set; }
    }
}
