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
    public class AppUserRoleEntityConfiguration : IEntityTypeConfiguration<AppUserRole>
    {
        public void Configure(EntityTypeBuilder<AppUserRole> builder)
        {
            builder
                .HasOne(x => x.User)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(x => x.Role)
                .WithMany()
                .HasForeignKey(x => x.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasDiscriminator<string>("Discriminator")
                .HasValue<AppUserRole>("AppUserRole");
        }
    }
}
