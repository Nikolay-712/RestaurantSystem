namespace RestaurantSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using RestaurantSystem.Data.Models.Contacts;

    public class AppMessageConfiguration : IEntityTypeConfiguration<AppMessage>
    {
        public void Configure(EntityTypeBuilder<AppMessage> appMessage)
        {
            appMessage
               .HasKey(x => x.Id);

            appMessage
                .Property(x => x.MessageType)
                .IsRequired();

            appMessage
                .Property(x => x.Message)
                .IsRequired()
                .HasMaxLength(700)
                .IsUnicode();

            appMessage
                .Property(x => x.Status)
                .IsRequired();

            appMessage
                .HasMany(x => x.Replies)
                .WithOne()
                .HasForeignKey(x => x.MessageId);
        }
    }
}
