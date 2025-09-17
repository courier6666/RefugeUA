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
    public class VolunteerEventScheduleItemEntityConfiguration : IEntityTypeConfiguration<VolunteerEventScheduleItem>
    {
        public void Configure(EntityTypeBuilder<VolunteerEventScheduleItem> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.StartTime).
                IsRequired();

            builder.Property(i => i.Description).
                HasMaxLength(400).
                IsRequired();

            builder.HasOne(i => i.VolunteerEvent).
                WithMany(e => e.ScheduleItems).
                HasForeignKey(i => i.VolunteerEventId).
                IsRequired();
        }
    }
}
