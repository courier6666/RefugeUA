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
    public class AddressEntityConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(a => a.Country).
                IsRequired().
                HasMaxLength(100);

            builder.Property(a => a.Region).
                IsRequired().
                HasMaxLength(100);

            builder.Property(a => a.District).
                IsRequired().
                HasMaxLength(100);

            builder.Property(a => a.Settlement).
                IsRequired().
                HasMaxLength(100);

            builder.Property(a => a.Street).
                IsRequired().
                HasMaxLength(100);

            builder.Property(a => a.PostalCode).
                IsRequired().
                HasMaxLength(5).
                IsFixedLength();

            builder.HasOne(a => a.Announcement).
                WithOne(a => a.Address).
                HasForeignKey<Announcement>(a => a.AddressId).
                IsRequired();

            builder.HasOne(a => a.VolunteerEvent).
                WithOne(v => v.Address).
                HasForeignKey<VolunteerEvent>(v => v.AddressId).
                IsRequired(false);
        }
    }
}
