namespace RestaurantSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using RestaurantSystem.Data.Models.Users;

    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> address)
        {
            address
                .HasKey(x => x.Id);

            address
                .Property(x => x.Country)
                .IsRequired();

            address
                .Property(x => x.Town)
                .IsRequired();

            address
                .Property(x => x.UseId)
                .IsRequired();

            address
                .Property(x => x.ShippingAddress)
                .IsRequired()
                .HasMaxLength(150)
                .IsUnicode();
        }
    }
}
