namespace RefugeUA.WebApp.Server.Features.Announcements.Accomodation.Common
{
    public class ListAccomodationAnnouncementQuery
    {
        public bool? IsClosed { get; set; }

        public string? District { get; set; }

        public string? AnnouncementGroup { get; set; }

        public string? Prompt { get; set; }

        public float? PriceLower { get; set; }

        public float? PriceUpper { get; set; }

        public bool? IsFree { get; set; }

        public float? AreaSqMetersLower { get; set; }

        public float? AreaSqMetersUpper { get; set; }

        public ushort? Capacity { get; set; }

        public ushort? Floors { get; set; }

        public ushort? NumberOfRooms { get; set; }

        public bool? PetsAllowed { get; set; }

        public string[]? BuildingTypes { get; set; }
    }
}
