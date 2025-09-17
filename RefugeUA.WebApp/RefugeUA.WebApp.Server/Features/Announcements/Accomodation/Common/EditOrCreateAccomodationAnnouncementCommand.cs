using Microsoft.AspNetCore.Mvc;
using RefugeUA.WebApp.Server.Features.Announcements.Common;

namespace RefugeUA.WebApp.Server.Features.Announcements.Accomodation.Common
{
    public class EditOrCreateAccomodationAnnouncementCommand : BaseEditOrCreateAnnouncementCommand
    {
        public string BuildingType { get; set; } = default!;

        public bool PetsAllowed { get; set; }

        public ushort Floors { get; set; }

        public ushort NumberOfRooms { get; set; }

        public ushort Capacity { get; set; }

        public float? AreaSqMeters { get; set; }

        public float? Price { get; set; }

        [FromForm]
        public List<IFormFile> Images { get; set; }
    }
}
