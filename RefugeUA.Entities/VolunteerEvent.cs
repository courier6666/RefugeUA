using RefugeUA.Entities.Abstracts;
using RefugeUA.Entities.Enums;
using RefugeUA.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefugeUA.Entities
{
    public class VolunteerEvent : Entity
    {
        public VolunteerEvent()
        {
            this.CreatedAt = DateTime.Now;
        }

        public string Title { get; set; } = default!;

        public string Content { get; set; } = default!;

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<IUser> Participants { get; set; } = default!;

        public ICollection<IUser> Organizers { get; set; } = default!;

        public ICollection<VolunteerEventScheduleItem>? ScheduleItems { get; set; }

        public long? AddressId { get; set; }

        public Address? Address { get; set; } = default!;

        public long? VolunteerGroupId { get; set; }

        public VolunteerGroup VolunteerGroup { get; set; } = default!;

        public string? OnlineConferenceLink { get; set; }

        public bool IsClosed { get; set; }

        public VolunteerEventType EventType { get; set; }

        public string? DonationLink { get; set; }
    }
}
