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
    public class EducationAnnouncementEntityConfiguration : IEntityTypeConfiguration<EducationAnnouncement>
    {
        public void Configure(EntityTypeBuilder<EducationAnnouncement> builder)
        {
            builder.Property(e => e.EducationType)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(e => e.InstitutionName)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(e => e.TargetGroup)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(e => e.IsFree)
                   .IsRequired();

            builder.Property(e => e.Fee).
                HasColumnType("decimal(18, 2)");

            builder.Property(e => e.Duration)
                   .IsRequired()
                   .HasMaxLength(2191);

            builder.Property(e => e.Language)
                   .IsRequired()
                   .HasMaxLength(50);
        }
    }
}
