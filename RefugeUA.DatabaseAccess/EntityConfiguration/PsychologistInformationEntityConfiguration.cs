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
    public class PsychologistInformationEntityConfiguration : IEntityTypeConfiguration<PsychologistInformation>
    {
        public void Configure(EntityTypeBuilder<PsychologistInformation> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Title).
                HasMaxLength(200);

            builder.HasIndex(p => p.Title).
                IsUnique();

            builder.Property(p => p.Description).
                HasMaxLength(1024).
                IsRequired();

            builder.Property(p => p.CreatedAt).
                IsRequired();

            builder.HasOne(p => p.Author as AppUser).
                WithMany().
                HasForeignKey(p => p.AuthorId);
        }
    }
}
