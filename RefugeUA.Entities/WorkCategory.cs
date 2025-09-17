using RefugeUA.Entities.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefugeUA.Entities
{
    public class WorkCategory : Entity
    {
        public string Name { get; set; } = default!;

        public ICollection<WorkAnnouncement> WorkAnnouncements { get; set; } = default!;
    }
}
