using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RefugeUA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefugeUA.DatabaseAccess.EntityConfiguration
{
    public class AnnouncementGroupEntityConfiguration : IEntityTypeConfiguration<AnnouncementGroup>
    {
        public void Configure(EntityTypeBuilder<AnnouncementGroup> builder)
        {
            builder.HasKey(g => g.Id);

            builder.Property(g => g.Name).
                IsRequired().
                HasMaxLength(100);

            builder.HasIndex(g => g.Name).
                IsUnique();

            builder.HasMany(g => g.Announcements).
                WithMany(a => a.Groups);
        }
    }
}
