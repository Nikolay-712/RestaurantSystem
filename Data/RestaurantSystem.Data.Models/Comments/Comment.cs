namespace RestaurantSystem.Data.Models.Comments
{
    using System;

    public class Comment
    {
        public Comment()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Text { get; set; }

        public string UserId { get; set; }

        public string RestaurantId { get; set; }
    }
}
