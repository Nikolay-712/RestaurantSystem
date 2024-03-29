﻿namespace RestaurantSystem.Data
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using RestaurantSystem.Data.Models.Comments;
    using RestaurantSystem.Data.Models.Contacts;
    using RestaurantSystem.Data.Models.Images;
    using RestaurantSystem.Data.Models.Notifications;
    using RestaurantSystem.Data.Models.Orders;
    using RestaurantSystem.Data.Models.Payments;
    using RestaurantSystem.Data.Models.Products;
    using RestaurantSystem.Data.Models.Ratings;
    using RestaurantSystem.Data.Models.Reservations;
    using RestaurantSystem.Data.Models.Restaurants;
    using RestaurantSystem.Data.Models.Users;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Restaurant> Restaurants { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<AppMessage> AppMessages { get; set; }

        public DbSet<MessageReply> Replies { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderProducts> OrderProducts { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public override int SaveChanges() => this.SaveChanges(true);

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            this.SaveChangesAsync(true, cancellationToken);

        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Needed for Identity models configuration
            base.OnModelCreating(builder);

            this.ConfigureUserIdentityRelations(builder);

            var entityTypes = builder.Model.GetEntityTypes().ToList();

            // Disable cascade delete
            var foreignKeys = entityTypes
                .SelectMany(e => e.GetForeignKeys().Where(f => f.DeleteBehavior == DeleteBehavior.Cascade));
            foreach (var foreignKey in foreignKeys)
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        // Applies configurations
        private void ConfigureUserIdentityRelations(ModelBuilder builder)
             => builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }
}
