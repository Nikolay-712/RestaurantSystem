namespace RestaurantSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using RestaurantSystem.Data.Models.Notifications;

    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> notification)
        {
            notification
                .HasKey(x => x.Id);

            notification
                .Property(x => x.Message)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(50);

            notification
                .Property(x => x.TargetId)
                .IsRequired();

            notification
                .HasOne(x => x.User)
                .WithMany(x => x.Notifications)
                .HasForeignKey(x => x.UserId);
        }
    }
}
