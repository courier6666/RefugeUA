using RefugeUA.Entities.Interfaces;
using RefugeUA.WebApp.Server.Features.Announcements.Common;
using RefugeUA.WebApp.Server.Shared.Dto.Address;
using RefugeUA.WebApp.Server.Shared.Dto.ContactInformation;
using RefugeUA.WebApp.Server.Shared.Dto.User;

namespace RefugeUA.WebApp.Server.Shared.Dto.BaseAnnouncement
{
    public class BaseAnnouncementResult
    {
        public long Id { get; set; }

        public string Title { get; set; } = default!;

        public string Content { get; set; } = default!;

        public DateTime CreatedAt { get; set; }

        public long AuthorId { get; set; }

        public UserDtoWithId Author { get; set; } = default!;

        public AddressDtoWithId Address { get; set; } = default!;

        public ContactInformationDtoWithId ContactInformation { get; set; } = default!;

        public bool? IsAccepted { get; set; }

        public string? NonAcceptenceReason { get; set; }

        public int ResponsesCount { get; set; }

        public bool IsClosed { get; set; }

        public IEnumerable<AnnouncementGroupDtoWithId> AnnouncementGroups { get; set; } = default!;
    }
}
