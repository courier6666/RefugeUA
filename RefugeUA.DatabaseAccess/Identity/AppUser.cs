using Microsoft.AspNetCore.Identity;
using RefugeUA.Entities;
using RefugeUA.Entities.Abstracts;
using RefugeUA.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefugeUA.DatabaseAccess.Identity
{
    public class AppUser : IdentityUser<long>, IUser
    {
        public AppUser()
        {
            this.CreatedAt = DateTime.Now;
        }

        public ICollection<VolunteerGroup> FollowerVolunteerGroups { get; set; } = default!;

        public ICollection<VolunteerGroup> AdminVolunteerGroups { get; set; } = default!;

        public ICollection<AnnouncementResponse> AnnouncementResponses { get; set; } = default!;

        public string FirstName { get; set; } = default!;

        public string LastName { get; set; } = default!;

        public DateTime DateOfBirth { get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<Announcement> Announcements { get; set; } = default!;

        public ICollection<VolunteerEvent> OrganizedEvents { get; set; } = default!;

        public ICollection<VolunteerEvent> ParticipatedEvents { get; set; } = default!;
        
        public string? ProfileImageUrl { get; set; }

        string IUser.Email { get => this.Email!; set => this.Email = value; }

        string IUser.PhoneNumber { get => this.PhoneNumber!; set => this.PhoneNumber = value; }

        public bool IsAccepted { get; set; }

        public string? District { get; set; } = default!;

        public virtual ICollection<AppUserRole> UserRoles { get; set; } = default!;
    }
}
