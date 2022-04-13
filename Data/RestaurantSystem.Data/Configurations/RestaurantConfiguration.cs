namespace RestaurantSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using RestaurantSystem.Data.Models.Restaurants;

    public class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> restaurant)
        {
            restaurant
                .HasKey(x => x.Id);

            restaurant
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode();

            restaurant
                .Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode();

            restaurant
                .Property(x => x.DeliveryPeice)
                .IsRequired()
                .HasColumnType<decimal>("decimal")
                .HasPrecision(10, 2);

            restaurant
                .Property(x => x.OwnerId)
                .IsRequired();

            restaurant
                .HasMany(x => x.Menu)
                .WithOne()
                .HasForeignKey(x => x.RestaurantId);

            restaurant
                .HasOne(x => x.Owner)
                .WithMany(x => x.Restaurants)
                .HasForeignKey(x => x.OwnerId);
        }
    }
}
