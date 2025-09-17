using RefugeUA.Entities.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefugeUA.Entities.Interfaces
{
    public interface IUser : IEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime CreatedAt { get; set; }

        public string? ProfileImageUrl { get; set; }

        public ICollection<VolunteerGroup> FollowerVolunteerGroups { get; set; }

        public ICollection<VolunteerGroup> AdminVolunteerGroups { get; set; }

        public ICollection<AnnouncementResponse> AnnouncementResponses { get; set; }

        public ICollection<Announcement> Announcements { get; set; }

        public ICollection<VolunteerEvent> OrganizedEvents { get; set; }

        public ICollection<VolunteerEvent> ParticipatedEvents { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsAccepted { get; set; }

        public string? District { get; set; }
    }
}
