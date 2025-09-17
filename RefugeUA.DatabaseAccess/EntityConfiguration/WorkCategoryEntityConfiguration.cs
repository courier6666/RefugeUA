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
    public class WorkCategoryEntityConfiguration : IEntityTypeConfiguration<WorkCategory>
    {
        public void Configure(EntityTypeBuilder<WorkCategory> builder)
        {
            builder.HasKey(w => w.Id);
            builder.Property(w => w.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasMany(w => w.WorkAnnouncements)
                .WithOne(a => a.WorkCategory)
                .HasForeignKey(a => a.WorkCategoryId);
        }
    }
}
