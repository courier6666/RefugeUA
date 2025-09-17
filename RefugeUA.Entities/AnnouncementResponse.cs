using RefugeUA.Entities.Abstracts;
using RefugeUA.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefugeUA.Entities
{
    public class AnnouncementResponse : Entity
    {
        public AnnouncementResponse()
        {
            this.CreatedAt = DateTime.Now;
        }

        public long? UserId { get; set; }

        public IUser? User { get; set; } = default!;

        public long AnnouncementId { get; set; }

        public Announcement Announcement { get; set; } = default!;

        public long ContactInformationId { get; set; }

        public ContactInformation ContactInformation { get; set; } = default!;

        public DateTime CreatedAt { get; set; }
    }
}
