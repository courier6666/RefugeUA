using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefugeUA.DatabaseAccess.Identity
{
    public class AppUserRole : IdentityUserRole<long>
    {
        public virtual AppUser User { get; set; } = default!;
        public virtual AppRole Role { get; set; } = default!;
    }
}
