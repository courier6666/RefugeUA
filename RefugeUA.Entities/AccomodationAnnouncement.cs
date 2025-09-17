using RefugeUA.Entities.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefugeUA.Entities
{
    public class AccomodationAnnouncement : Announcement
    {
        public ICollection<Image> Images { get; set; } = default!;

        public string BuildingType { get; set; } = default!;

        public bool PetsAllowed { get; set; }

        public ushort Floors { get; set; }

        public ushort NumberOfRooms { get; set; }

        public ushort Capacity { get; set; }

        public float? AreaSqMeters { get; set; }

        public float? Price { get; set; }

        public bool IsFree { get; set; }
    }
}
