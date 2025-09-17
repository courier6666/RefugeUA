using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RefugeUA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefugeUA.DatabaseAccess.EntityConfiguration
{
    public class AccomodationAnnouncementEntityConfiguration : IEntityTypeConfiguration<AccomodationAnnouncement>
    {
        public void Configure(EntityTypeBuilder<AccomodationAnnouncement> builder)
        {
            builder.HasMany(a => a.Images).
                WithMany().
                UsingEntity(join => join.ToTable("AccomodationImages"));

            builder.Property(a => a.BuildingType).
                HasMaxLength(100).
                IsRequired();

            builder.Property(a => a.Floors).
                IsRequired();

            builder.Property(a => a.NumberOfRooms).
                IsRequired();

            builder.Property(a => a.Capacity).
                IsRequired();

            builder.Property(a => a.AreaSqMeters).
                IsRequired(false);

            builder.Property(a => a.Price).
                IsRequired(false).
                HasColumnType("decimal(18, 2)");
        }
    }
}
