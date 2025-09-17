using RefugeUA.Entities.Enums;
using RefugeUA.Entities;
using RefugeUA.WebApp.Server.Shared.Dto.User;
using RefugeUA.WebApp.Server.Shared.Dto.Address;

namespace RefugeUA.WebApp.Server.Features.Volunteer.Events.Common
{
    public class VolunteerEventBaseResult
    {
        public long Id { get; set; }

        public string Title { get; set; } = default!;

        public string Content { get; set; } = default!;

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public DateTime CreatedAt { get; set; }

        public long? AddressId { get; set; }

        public AddressDtoWithId Address { get; set; } = default!;

        public long? VolunteerGroupId { get; set; }

        public string? VolunteerGroupTitle { get; set; }

        public string? OnlineConferenceLink { get; set; }

        public bool IsClosed { get; set; }

        public VolunteerEventType EventType { get; set; }

        public string? DonationLink { get; set; }

        public IEnumerable<UserDtoWithId>? Organizers { get; set; }

        public int ParticipantsCount { get; set; }
    }
}
