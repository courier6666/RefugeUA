using RefugeUA.Entities.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefugeUA.Entities
{
    public class ContactInformation : Entity
    {
        public string PhoneNumber { get; set; } = default!;

        public string? Email { get; set; }

        public string? Telegram { get; set; }

        public string? Viber {  get; set; }

        public string? Facebook { get; set; }

        public Announcement? Announcement { get; set; } = default!;

        public PsychologistInformation? PsychologistInformation { get; set; } = default!;

        public AnnouncementResponse? AnnouncementResponse { get; set; } = default!;
    }
}
