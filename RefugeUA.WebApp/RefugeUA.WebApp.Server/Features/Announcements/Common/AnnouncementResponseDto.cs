using RefugeUA.WebApp.Server.Shared.Dto.ContactInformation;

namespace RefugeUA.WebApp.Server.Features.Announcements.Common
{
    /// <summary>
    /// Used as write model for creating an announcement.
    /// </summary>
    public class AnnouncementResponseDto
    {
        public ContactInformationDto ContactInformation { get; set; } = default!;
    }
}
