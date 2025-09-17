using RefugeUA.Entities.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefugeUA.Entities
{
    public class VolunteerEventScheduleItem : Entity
    {
        public DateTime StartTime { get; set; }

        public string Description { get; set; } = default!;

        public long VolunteerEventId { get; set; } = default!;

        public VolunteerEvent VolunteerEvent { get; set; } = default!;
    }
}
