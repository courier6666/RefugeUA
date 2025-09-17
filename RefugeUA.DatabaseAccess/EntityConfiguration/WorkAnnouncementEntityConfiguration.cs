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
    public class WorkAnnouncementEntityConfiguration : IEntityTypeConfiguration<WorkAnnouncement>
    {
        public void Configure(EntityTypeBuilder<WorkAnnouncement> builder)
        {
            builder.Property(w => w.JobPosition).
                HasMaxLength(200).
                IsRequired();

            builder.Property(w => w.CompanyName).
                HasMaxLength(400).
                IsRequired();

            builder.Property(w => w.SalaryLower).
                IsRequired(false).
                HasColumnType("decimal(18, 2)");

            builder.Property(w => w.SalaryUpper).
                IsRequired(false).
                HasColumnType("decimal(18, 2)");

            builder.Property(w => w.RequirementsContent).
                HasMaxLength(1024).
                IsRequired();

            builder.HasOne(w => w.WorkCategory).
                WithMany(c => c.WorkAnnouncements).
                HasForeignKey(w => w.WorkCategoryId);
        }
    }
}
