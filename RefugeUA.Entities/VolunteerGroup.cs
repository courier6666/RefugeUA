using RefugeUA.Entities.Abstracts;
using RefugeUA.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefugeUA.Entities
{
    public class VolunteerGroup : Entity
    {
        public VolunteerGroup()
        {
            this.CreatedAt = DateTime.Now;
        }

        public string Title { get; set; } = default!;

        public string DescriptionContent { get; set; } = default!;

        public DateTime CreatedAt { get; set; }

        public ICollection<IUser> Followers { get; set; } = default!;

        public ICollection<IUser> Admins { get; set; } = default!;

        public ICollection<VolunteerEvent> VolunteerEvents { get; set; } = default!;
    }
}
