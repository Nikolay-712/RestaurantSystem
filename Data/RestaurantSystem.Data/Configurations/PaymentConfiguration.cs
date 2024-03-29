﻿namespace RestaurantSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using RestaurantSystem.Data.Models.Payments;

    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> payment)
        {
            payment
                .HasKey(x => x.Id);

            payment
                .Property(x => x.PaymentType)
                .IsRequired();

            payment
                .Property(x => x.UserId)
                .IsRequired();

            payment
                .Property(x => x.Amount)
                .IsRequired()
                .HasColumnType<decimal>("decimal")
                .HasPrecision(10, 2);
        }
    }
}
