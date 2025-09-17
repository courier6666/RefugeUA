using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RefugeUA.DatabaseAccess.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefugeUA.DatabaseAccess.EntityConfiguration
{
    public class AppUserEntityConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(a => a.FirstName).
                IsRequired().
                HasMaxLength(100);

            builder.Property(a => a.LastName).
                IsRequired().
                HasMaxLength(100);

            builder.Property(a => a.ProfileImageUrl).
                HasMaxLength(200);

            builder.Property(a => a.CreatedAt).
                IsRequired();

            builder.Property(a => a.DateOfBirth).
                IsRequired();

            builder.HasIndex(a => a.Email).
                IsUnique();

            builder.HasIndex(a => a.PhoneNumber).
                IsUnique();

            builder.HasMany(a => a.Announcements).
                WithOne(a => a.Author as AppUser);

            builder.HasMany(a => a.AnnouncementResponses).
                WithOne(r => r.User as AppUser).
                HasForeignKey(r => r.UserId).
                IsRequired().
                OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.UserRoles).
                WithOne(ur => ur.User).
                HasForeignKey(ur => ur.UserId).
                IsRequired();
        }
    }
}