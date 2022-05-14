namespace RestaurantSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using RestaurantSystem.Data.Models.Orders;
    using RestaurantSystem.Data.Models.Payments;

    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> order)
        {
            order
                .HasKey(x => x.Id);
            order
               .Property(x => x.UserId)
               .IsRequired();

            order
                .Property(x => x.ResaurantId)
                .IsRequired();

            order
                .Property(x => x.PaymentId)
                .IsRequired();

            order
                .HasOne(x => x.Payment)
                .WithOne()
                .HasForeignKey<Payment>(x => x.Id);
        }
    }
}
