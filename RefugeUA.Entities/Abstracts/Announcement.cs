using RefugeUA.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefugeUA.Entities.Abstracts
{
    public class Announcement : Entity
    {
        protected Announcement()
        {
            this.CreatedAt = DateTime.Now;
        }

        public string Title { get; set; } = default!;

        public string Content { get; set; } = default!;

        public long ContactInformationId { get; set; }

        public ContactInformation ContactInformation { get; set; } = default!;

        public long AddressId { get; set; }

        public Address Address { get; set; } = default!;

        public ICollection<AnnouncementResponse> Responses { get; set; } = default!;

        public ICollection<AnnouncementGroup> Groups { get; set; } = default!;
        
        public DateTime CreatedAt { get; set; }

        public long? AuthorId { get; set; }

        public IUser? Author { get; set; } = default!;

        public bool Accepted { get; set; }

        public string? NonAcceptenceReason { get; set; }

        public bool IsClosed { get; set; }
    }
}
