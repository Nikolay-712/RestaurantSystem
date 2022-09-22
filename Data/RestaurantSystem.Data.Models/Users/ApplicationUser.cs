namespace RestaurantSystem.Data.Models.Users
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;
    using RestaurantSystem.Data.Models.Comments;
    using RestaurantSystem.Data.Models.Contacts;
    using RestaurantSystem.Data.Models.Notifications;
    using RestaurantSystem.Data.Models.Orders;
    using RestaurantSystem.Data.Models.Ratings;
    using RestaurantSystem.Data.Models.Reservations;
    using RestaurantSystem.Data.Models.Restaurants;

    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.Restaurants = new HashSet<Restaurant>();
            this.Reservations = new HashSet<Reservation>();
            this.Messages = new HashSet<AppMessage>();
            this.Orders = new HashSet<Order>();
            this.Ratings = new HashSet<Rating>();
            this.Notifications = new HashSet<Notification>();
            this.Comments = new HashSet<Comment>();
        }

        public DateTime CreatedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public ICollection<IdentityUserRole<string>> Roles { get; set; }

        public ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public ICollection<Restaurant> Restaurants { get; set; }

        public ICollection<Reservation> Reservations { get; set; }

        public ICollection<AppMessage> Messages { get; set; }

        public ICollection<Order> Orders { get; set; }

        public ICollection<Rating> Ratings { get; set; }

        public ICollection<Notification> Notifications { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public string AddressId { get; set; }

        public Address Address { get; set; }
    }
}
