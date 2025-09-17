using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefugeUA.DatabaseAccess.Identity
{
    public class AppRole : IdentityRole<long>
    {
        public AppRole()
        {
            
        }

        public AppRole(string roleName) : base(roleName)
        {
            
        }

        public virtual ICollection<AppUserRole> UserRoles { get; set; } = default!;
    }
}
