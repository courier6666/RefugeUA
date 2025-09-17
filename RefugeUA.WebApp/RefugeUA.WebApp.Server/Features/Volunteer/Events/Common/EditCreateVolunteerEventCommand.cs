using RefugeUA.Entities.Enums;
using RefugeUA.Entities.Interfaces;
using RefugeUA.Entities;
using RefugeUA.WebApp.Server.Shared.Dto.Address;

namespace RefugeUA.WebApp.Server.Features.Volunteer.Events.Common
{
    public class EditCreateVolunteerEventCommand
    {
        public string Title { get; set; } = default!;

        public string Content { get; set; } = default!;

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public VolunteerEventScheduleItemDtoWithId[]? ScheduleItems { get; set; }

        public AddressDto? Address { get; set; } = default!;

        public long? VolunteerGroupId { get; set; }

        public string? OnlineConferenceLink { get; set; }

        public VolunteerEventType EventType { get; set; }

        public string? DonationLink { get; set; }
    }
}
