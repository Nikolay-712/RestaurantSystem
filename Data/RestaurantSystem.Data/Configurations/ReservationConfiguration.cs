namespace RestaurantSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using RestaurantSystem.Data.Models.Reservations;

    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> reservation)
        {
            reservation
                .HasKey(x => x.Id);

            reservation
                .Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode();

            reservation
                .Property(x => x.PhoneNumber)
                .IsRequired()
                .HasMaxLength(13);
        }
    }
}
