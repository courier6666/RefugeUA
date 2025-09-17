namespace RefugeUA.WebApp.Server.Features.Volunteer.Events.Common
{
    public class VolunteerEventScheduleItemDtoWithId
    {
        public long Id { get; set; }

        public DateTime StartTime { get; set; }

        public string Description { get; set; } = default!;
    }
}
