using RefugeUA.WebApp.Server.Shared.Dto.BaseAnnouncement;

namespace RefugeUA.WebApp.Server.Features.Announcements.Accomodation.Common
{
    public class AccomodationAnnouncementResult : BaseAnnouncementResult
    {
        public string BuildingType { get; set; } = default!;

        public bool PetsAllowed { get; set; }

        public ushort Floors { get; set; }

        public ushort NumberOfRooms { get; set; }

        public ushort Capacity { get; set; }

        public float? AreaSqMeters { get; set; }

        public float? Price { get; set; }

        public bool IsFree { get; set; }

        public ImageResult[]? Images { get; set; }
    }
}
