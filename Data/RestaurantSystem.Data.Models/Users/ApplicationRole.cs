﻿namespace RestaurantSystem.Data.Models.Users
{
    using System;

    using Microsoft.AspNetCore.Identity;

    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole()
            : this(null)
        {
        }

        public ApplicationRole(string name)
            : base(name)
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public DateTime CreatedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
