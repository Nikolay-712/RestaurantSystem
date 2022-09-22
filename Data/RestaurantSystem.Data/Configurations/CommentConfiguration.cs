namespace RestaurantSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using RestaurantSystem.Data.Models.Comments;

    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> comment)
        {
            comment
                .HasKey(x => x.Id);

            comment
                .Property(x => x.Text)
                .IsRequired()
                .HasMaxLength(300)
                .IsUnicode();

            comment
                .Property(x => x.RestaurantId)
                .IsRequired();

            comment
                .Property(x => x.UserId)
                .IsRequired();
        }
    }
}
