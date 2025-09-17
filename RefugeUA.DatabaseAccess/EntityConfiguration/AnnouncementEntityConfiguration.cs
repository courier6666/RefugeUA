using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RefugeUA.DatabaseAccess.Identity;
using RefugeUA.Entities.Abstracts;

namespace RefugeUA.DatabaseAccess.EntityConfiguration
{
    public class AnnouncementEntityConfiguration : IEntityTypeConfiguration<Announcement>
    {
        public void Configure(EntityTypeBuilder<Announcement> builder)
        {
            builder.UseTptMappingStrategy();

            builder.HasKey(a => a.Id);

            builder.Property(a => a.CreatedAt).
                IsRequired();

            builder.Property(a => a.Title).
                IsRequired().
                HasMaxLength(200);

            builder.Property(a => a.Content).
                IsRequired().
                HasMaxLength(4096);

            builder.Property(a => a.AuthorId).
                IsRequired(false);

            builder.Property(a => a.NonAcceptenceReason).
                HasMaxLength(500).
                IsRequired(false);

            builder.HasOne(a => a.Author as AppUser).
                WithMany(a => a.Announcements).
                HasForeignKey(a => a.AuthorId).
                OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(a => a.Responses).
                WithOne(r => r.Announcement).
                HasForeignKey(r => r.AnnouncementId).
                OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(a => a.Groups).
                WithMany(g => g.Announcements).
                UsingEntity(join => join.ToTable("AnnouncementToGroup"));
        }
    }
}
