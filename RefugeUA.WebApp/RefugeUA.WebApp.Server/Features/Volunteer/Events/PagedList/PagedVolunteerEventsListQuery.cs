using RefugeUA.Entities.Enums;
using RefugeUA.WebApp.Server.Shared.Dto.PagingInfo;

namespace RefugeUA.WebApp.Server.Features.Volunteer.Events.PagedList
{
    public class PagedVolunteerEventsListQuery : IPagingInfoQuery
    {
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string? Prompt { get; set; }

        public VolunteerEventType? EventType { get; set; }

        public long? VolunteerGroupId { get; set; }

        public string? District { get; set; }

        public bool? IsClosed { get; set; }

        public int Page { get; set; }

        public int PageLength { get; set; }
    }
}
