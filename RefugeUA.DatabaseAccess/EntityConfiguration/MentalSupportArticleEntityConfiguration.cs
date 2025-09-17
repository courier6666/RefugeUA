using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RefugeUA.DatabaseAccess.Identity;
using RefugeUA.Entities;

namespace RefugeUA.DatabaseAccess.EntityConfiguration
{
    public class MentalSupportArticleEntityConfiguration : IEntityTypeConfiguration<MentalSupportArticle>
    {
        public void Configure(EntityTypeBuilder<MentalSupportArticle> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.CreatedAt).
                IsRequired();

            builder.Property(a => a.Title).
                HasMaxLength(200);

            builder.HasIndex(a => a.Title).
                IsUnique();

            builder.Property(a => a.Content).
                HasMaxLength(8192);

            builder.HasOne(a => a.Author as AppUser).
                WithMany().
                HasForeignKey(a => a.AuthorId);
        }
    }
}
