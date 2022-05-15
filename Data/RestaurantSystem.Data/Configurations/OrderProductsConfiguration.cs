namespace RestaurantSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using RestaurantSystem.Data.Models.Orders;

    public class OrderProductsConfiguration : IEntityTypeConfiguration<OrderProducts>
    {
        public void Configure(EntityTypeBuilder<OrderProducts> orderProducts)
        {
            orderProducts
                .HasKey(x => new { x.OrderId, x.ProductId });

            orderProducts
                .HasOne(x => x.Order)
                .WithMany(x => x.OrderProducts)
                .HasForeignKey(x => x.OrderId);

            orderProducts
                .HasOne(x => x.Product)
                .WithMany(x => x.OrderProducts)
                .HasForeignKey(x => x.ProductId);
        }
    }
}
