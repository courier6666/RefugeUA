using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RefugeUA.Entities;
using RefugeUA.Entities.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefugeUA.DatabaseAccess.EntityConfiguration
{
    public class ContactInformationEntityConfiguration : IEntityTypeConfiguration<ContactInformation>
    {
        public void Configure(EntityTypeBuilder<ContactInformation> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.PhoneNumber).
                HasMaxLength(16).
                IsRequired();

            builder.Property(c => c.Email).
                HasMaxLength(128).
                IsRequired(false);

            builder.Property(c => c.Telegram).
                HasMaxLength(256).
                IsRequired(false);

            builder.Property(c => c.Viber).
                HasMaxLength(256).
                IsRequired(false);

            builder.Property(c => c.Facebook).
                HasMaxLength(256).
                IsRequired(false);

            builder.HasOne(c => c.PsychologistInformation).
                WithOne(p => p.Contact).
                HasForeignKey<PsychologistInformation>(p => p.ContactId).
                IsRequired();

            builder.HasOne(c => c.AnnouncementResponse).
                WithOne(r => r.ContactInformation).
                HasForeignKey<AnnouncementResponse>(a => a.ContactInformationId).
                IsRequired().
                OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(c => c.Announcement).
                WithOne(a => a.ContactInformation).
                HasForeignKey<Announcement>(a => a.ContactInformationId).
                IsRequired();
        }
    }
}
