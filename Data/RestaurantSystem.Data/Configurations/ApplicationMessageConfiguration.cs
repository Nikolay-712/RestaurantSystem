namespace RestaurantSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using RestaurantSystem.Data.Models.Contacts;

    public class ApplicationMessageConfiguration : IEntityTypeConfiguration<ApplicationMessage>
    {
        public void Configure(EntityTypeBuilder<ApplicationMessage> appMessage)
        {
            appMessage
                .HasKey(x => x.Id);

            appMessage
                .Property(x => x.Sender)
                .IsRequired()
                .HasMaxLength(100);

            appMessage
                .Property(x => x.MessageType)
                .IsRequired();

            appMessage
                .Property(x => x.Message)
                .IsRequired()
                .HasMaxLength(700)
                .IsUnicode();
        }
    }
}
