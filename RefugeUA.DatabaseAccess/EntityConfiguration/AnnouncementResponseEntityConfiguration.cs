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
    public class AnnouncementResponseEntityConfiguration : IEntityTypeConfiguration<AnnouncementResponse>
    {
        public void Configure(EntityTypeBuilder<AnnouncementResponse> builder)
        {
            builder.HasKey(r => r.Id);

            builder.HasOne(r => r.User as AppUser).
                WithMany(a => a.AnnouncementResponses).
                HasForeignKey(r => r.UserId).
                OnDelete(DeleteBehavior.Cascade);

            builder.Property(r => r.CreatedAt).
                IsRequired();
        }
    }
}
