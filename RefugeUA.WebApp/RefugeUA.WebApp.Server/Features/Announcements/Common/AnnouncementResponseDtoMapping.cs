using RefugeUA.Entities;
using RefugeUA.WebApp.Server.Shared.Dto.ContactInformation;
using RefugeUA.WebApp.Server.Shared.Dto.User;
using System.Linq.Expressions;

namespace RefugeUA.WebApp.Server.Features.Announcements.Common
{
    public static class AnnouncementResponseDtoMapping
    {
        public readonly static Expression<Func<AnnouncementResponse, AnnouncementResponseDtoWithId>> Expression =
            r => new AnnouncementResponseDtoWithId()
            {
                Id = r.Id,
                AnnouncementId = r.AnnouncementId,
                CreatedAt = r.CreatedAt,
                ContactInformationId = r.ContactInformationId,
                ContactInformation = new ContactInformationDtoWithId()
                {
                    Id = r.ContactInformation.Id,
                    PhoneNumber = r.ContactInformation.PhoneNumber,
                    Email = r.ContactInformation.Email,
                    Telegram = r.ContactInformation.Telegram,
                    Facebook = r.ContactInformation.Facebook,
                    Viber = r.ContactInformation.Viber,
                },
                UserId = r.UserId ?? 0,
                User = new UserDtoWithId()
                {
                    Id = r.User!.Id,
                    FirstName = r.User!.FirstName,
                    LastName = r.User!.LastName,
                    CreatedAt = r.User!.CreatedAt,
                    DateOfBirth = r.User!.DateOfBirth,
                    Email = r.User!.Email,
                    PhoneNumber = r.User!.PhoneNumber,
                },
            };
    }
}
