using RefugeUA.Entities;
using RefugeUA.Entities.Abstracts;
using RefugeUA.WebApp.Server.Shared.Dto.BaseAnnouncement;
using RefugeUA.WebApp.Server.Shared.Dto.ContactInformation;
using RefugeUA.WebApp.Server.Shared.Dto.User;
using System.Linq.Expressions;

namespace RefugeUA.WebApp.Server.Features.Announcements.Common
{
    public static class AnnouncementResponseWithAnnouncementDtoMapping
    {
        public readonly static Expression<Func<AnnouncementResponse, AnnouncementResponseWithAnnouncementDtoWithId>> Expression =
     r => new AnnouncementResponseWithAnnouncementDtoWithId()
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
         Announcement = new BaseAnnouncementResultWithType()
         {
             Id = r.Announcement.Id,
             Title = r.Announcement.Title,
             Content = r.Announcement.Content,
             CreatedAt = r.Announcement.CreatedAt,
             IsAccepted = r.Announcement.Accepted,
             NonAcceptenceReason = r.Announcement.NonAcceptenceReason,
             IsClosed = r.Announcement.IsClosed,
             AuthorId = r.Announcement.AuthorId ?? 0,
         }
     };

        public readonly static Func<AnnouncementResponse, AnnouncementResponseWithAnnouncementDtoWithId> Func =
        r =>
        {
            AnnouncementType type = default;

            switch (r.Announcement)
            {
                case WorkAnnouncement workAnnouncement:
                    type = AnnouncementType.Work;
                    break;
                case AccomodationAnnouncement accomodationAnnouncement:
                    type = AnnouncementType.Accomodation;
                    break;
                case EducationAnnouncement educationAnnouncement:
                    type = AnnouncementType.Education;
                    break;
            }

            return new AnnouncementResponseWithAnnouncementDtoWithId()
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
                Announcement = new BaseAnnouncementResultWithType()
                {
                    Id = r.Announcement.Id,
                    Title = r.Announcement.Title,
                    Content = r.Announcement.Content,
                    CreatedAt = r.Announcement.CreatedAt,
                    IsAccepted = r.Announcement.Accepted,
                    NonAcceptenceReason = r.Announcement.NonAcceptenceReason,
                    IsClosed = r.Announcement.IsClosed,
                    AuthorId = r.Announcement.AuthorId ?? 0,
                    Type = type,
                }
            };
        };
    }
}
