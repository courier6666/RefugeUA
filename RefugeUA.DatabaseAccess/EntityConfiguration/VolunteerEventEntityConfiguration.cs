using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RefugeUA.DatabaseAccess.Identity;
using RefugeUA.Entities;
using RefugeUA.Entities.Abstracts;
using RefugeUA.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefugeUA.DatabaseAccess.EntityConfiguration
{
    public class VolunteerEventEntityConfiguration : IEntityTypeConfiguration<VolunteerEvent>
    {
        public void Configure(EntityTypeBuilder<VolunteerEvent> builder)
        {
            builder.HasKey(v => v.Id);

            builder.Property(v => v.Title)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(v => v.Content)
                .HasMaxLength(4096)
                .IsRequired();

            builder.Property(v => v.StartTime)
                .IsRequired(false);

            builder.Property(v => v.EndTime)
                .IsRequired();

            builder.Property(v => v.CreatedAt)
                .IsRequired();

            builder.Property(v => v.OnlineConferenceLink)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(v => v.DonationLink)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(v => v.AddressId).
                IsRequired(false);

            builder.Property(v => v.EventType).
                HasDefaultValue(VolunteerEventType.Participation);

            builder.HasOne(v => v.VolunteerGroup)
                .WithMany(g => g.VolunteerEvents)
                .HasForeignKey(v => v.VolunteerGroupId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(v => v.ScheduleItems)
                .WithOne(s => s.VolunteerEvent)
                .HasForeignKey(s => s.VolunteerEventId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(v => v.Organizers as IEnumerable<AppUser>).
                WithMany(a => a.OrganizedEvents).
                UsingEntity(join => join.ToTable("EventOrganizers"));

            builder.HasMany(v => v.Participants as IEnumerable<AppUser>).
                WithMany(a => a.ParticipatedEvents).
                UsingEntity(join => join.ToTable("EventParticipants"));


        }
    }
}
