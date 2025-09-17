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
    public class ImageEntityConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Path).
                HasMaxLength(200).
                IsRequired();

            builder.HasIndex(i => i.Path).
                IsUnique();
        }
    }
}
