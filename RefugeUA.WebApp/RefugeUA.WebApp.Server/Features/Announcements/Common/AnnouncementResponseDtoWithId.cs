using RefugeUA.WebApp.Server.Shared.Dto.ContactInformation;
using RefugeUA.WebApp.Server.Shared.Dto.User;

namespace RefugeUA.WebApp.Server.Features.Announcements.Common
{
    public class AnnouncementResponseDtoWithId
    {
        public long Id { get; set; }

        public long AnnouncementId { get; set; }

        public long ContactInformationId { get; set; }

        public ContactInformationDtoWithId ContactInformation { get; set; } = default!;

        public long UserId { get; set; }

        public UserDtoWithId User { get; set; } = default!;

        public DateTime CreatedAt { get; set; }
    }
}
