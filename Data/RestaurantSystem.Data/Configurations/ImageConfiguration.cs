namespace RestaurantSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using RestaurantSystem.Data.Models.Images;

    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> image)
        {
            image
                 .HasKey(x => x.Id);

            image
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            image
                .Property(x => x.ImageUrl)
                .IsRequired();

            image
                .Property(x => x.RestaurantId)
                .IsRequired();
        }
    }
}
