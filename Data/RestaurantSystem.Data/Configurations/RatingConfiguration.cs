namespace RestaurantSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using RestaurantSystem.Data.Models.Ratings;

    public class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> rating)
        {
            rating
                .HasKey(x => x.Id);

            rating
                .Property(x => x.Stars)
                .IsRequired();

            rating
                .Property(x => x.UserId)
                .IsRequired();
        }
    }
}
