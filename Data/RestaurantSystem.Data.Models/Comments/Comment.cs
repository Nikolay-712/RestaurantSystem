namespace RestaurantSystem.Data.Models.Comments
{
    using System;

    using RestaurantSystem.Data.Models.Users;

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

        public ApplicationUser User { get; set; }

        public string RestaurantId { get; set; }
    }
}
