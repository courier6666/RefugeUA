using RefugeUA.Entities.Interfaces;
using RefugeUA.Entities;
using RefugeUA.WebApp.Server.Shared.Dto.Address;
using RefugeUA.WebApp.Server.Shared.Dto.ContactInformation;

namespace RefugeUA.WebApp.Server.Features.Announcements.Common
{
    public abstract class BaseEditOrCreateAnnouncementCommand
    {
        public string Title { get; set; } = default!;

        public string Content { get; set; } = default!;

        public ContactInformationDto ContactInformation { get; set; } = default!;

        public AddressDto Address { get; set; } = default!;
    }
}
