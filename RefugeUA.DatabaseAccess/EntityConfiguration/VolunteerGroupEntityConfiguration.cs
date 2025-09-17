using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RefugeUA.DatabaseAccess.Identity;
using RefugeUA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefugeUA.DatabaseAccess.EntityConfiguration
{
    public class VolunteerGroupEntityConfiguration : IEntityTypeConfiguration<VolunteerGroup>
    {
        public void Configure(EntityTypeBuilder<VolunteerGroup> builder)
        {
            builder.HasKey(g => g.Id);

            builder.Property(g => g.Title).
                HasMaxLength(200).
                IsRequired();

            builder.HasIndex(g => g.Title).
                IsUnique();

            builder.Property(g => g.DescriptionContent).
                IsRequired().
                HasMaxLength(4096);

            builder.Property(g => g.CreatedAt).
                IsRequired();

            builder.HasMany(g => g.Followers as IEnumerable<AppUser>).
                WithMany(a => a.FollowerVolunteerGroups).
                UsingEntity(join => join.ToTable("VolunteerGroupFollowers"));

            builder.HasMany(g => g.Admins as IEnumerable<AppUser>).
                WithMany(a => a.AdminVolunteerGroups).
                UsingEntity(join => join.ToTable("VolunteerGroupAdmins"));

            builder.HasMany(g => g.VolunteerEvents).
                WithOne(e => e.VolunteerGroup).
                HasForeignKey(e => e.VolunteerGroupId).
                OnDelete(DeleteBehavior.SetNull);
        }
    }
}
