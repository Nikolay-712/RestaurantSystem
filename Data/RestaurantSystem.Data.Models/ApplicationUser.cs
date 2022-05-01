namespace RestaurantSystem.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;
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
        }

        public DateTime CreatedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public ICollection<IdentityUserRole<string>> Roles { get; set; }

        public ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public ICollection<Restaurant> Restaurants { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
    }
}
