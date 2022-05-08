namespace RestaurantSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using RestaurantSystem.Data.Models.Contacts;

    public class MessageReplyConfiguration : IEntityTypeConfiguration<MessageReply>
    {
        public void Configure(EntityTypeBuilder<MessageReply> reply)
        {
            reply
               .HasKey(x => x.Id);

            reply
                .Property(x => x.Text)
                .IsRequired()
                .HasMaxLength(700)
                .IsUnicode();

            reply
                .Property(x => x.Sender)
                .IsRequired();

            reply
                .Property(x => x.MessageId)
                .IsRequired();
        }
    }
}
