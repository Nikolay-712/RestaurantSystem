namespace RestaurantSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using RestaurantSystem.Data.Models.Products;

    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> product)
        {
            product
                .HasKey(x => x.Id);

            product
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode();

            product
                .Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode();

            product
                .Property(x => x.Price)
                .IsRequired()
                .HasColumnType<decimal>("decimal")
                .HasPrecision(10, 2);

            product
                .Property(x => x.Weight)
                .IsRequired();

            product
                .Property(x => x.InStock)
                .IsRequired();

            product
                .Property(x => x.Category)
                .IsRequired();

            product
                .Property(x => x.RestaurantId)
                .IsRequired();

            product
                .HasMany(x => x.Ratings)
                .WithOne()
                .HasForeignKey(x => x.ProductId);
        }
    }
}
