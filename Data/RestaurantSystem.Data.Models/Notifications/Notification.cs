namespace RestaurantSystem.Data.Models.Notifications
{
    using System;

    using RestaurantSystem.Data.Models.Users;

    public class Notification
    {
        public Notification()
        {
            this.Id = Guid.NewGuid().ToString();
            this.IsSeen = false;
        }

        public string Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public string Message { get; set; }

        public string OrderId { get; set; }

        public string ReservationId { get; set; }

        public bool IsSeen { get; set; }
    }
}
