using RefugeUA.Entities.Enums;
using RefugeUA.Entities;
using RefugeUA.WebApp.Server.Shared.Dto.User;

namespace RefugeUA.WebApp.Server.Features.Volunteer.Events.Common
{
    public class VolunteerEventDetailResult : VolunteerEventBaseResult
    {
        public IEnumerable<VolunteerEventScheduleItemDtoWithId>? ScheduleItems { get; set; }
    }
}
