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
                .Property(x => x.PhoneNumber)
                .IsRequired()
                .HasMaxLength(13);

            order
                .Property(x => x.PaymentId)
                .IsRequired();

            order
                .Property(x => x.AddressId)
                .IsRequired();

            order
                .HasOne(x => x.Address)
                .WithOne()
                .HasForeignKey<Address>(x => x.Id);

            order
                .HasOne(x => x.Payment)
                .WithOne()
                .HasForeignKey<Payment>(x => x.Id);
        }
    }
}
