using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RefugeUA.DatabaseAccess.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefugeUA.DatabaseAccess.EntityConfiguration
{
    public class AppRoleEntityConfiguration : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder.HasMany(r => r.UserRoles).
                WithOne(ur => ur.Role).
                HasForeignKey(ur => ur.RoleId).
                IsRequired();
            
            builder
                .HasDiscriminator<string>("Discriminator")
                .HasValue<AppRole>("AppRole");
        }
    }
}
