using RefugeUA.Entities.Abstracts;
using System.Linq.Expressions;
using RefugeUA.WebApp.Server.Shared.Dto.Address;
using RefugeUA.WebApp.Server.Shared.Dto.ContactInformation;
using RefugeUA.WebApp.Server.Features.Announcements.Common;
using RefugeUA.WebApp.Server.Shared.Dto.User;
using RefugeUA.Entities;

namespace RefugeUA.WebApp.Server.Shared.Dto.BaseAnnouncement
{
    public static class BaseAnnouncementResultWithTypeMapping
    {
        public static Expression<Func<Announcement, BaseAnnouncementResultWithType>> Expression =>
            announcement => new BaseAnnouncementResultWithType
            {
                Id = announcement.Id,
                Title = announcement.Title,
                Content = announcement.Content,
                CreatedAt = announcement.CreatedAt,
                IsAccepted = announcement.Accepted,
                NonAcceptenceReason = announcement.NonAcceptenceReason,
                ResponsesCount = announcement.Responses.Count,
                IsClosed = announcement.IsClosed,
                Author = new UserDtoWithId()
                {
                    Id = announcement.AuthorId ?? 0,
                    FirstName = announcement.Author.FirstName,
                    LastName = announcement.Author.LastName,
                    CreatedAt = announcement.Author.CreatedAt,
                    DateOfBirth = announcement.Author.DateOfBirth,
                    Email = announcement.Author.Email,
                    PhoneNumber = announcement.Author.PhoneNumber,
                },
                Address = new AddressDtoWithId
                {
                    Id = announcement.Address.Id,
                    Country = announcement.Address.Country,
                    Region = announcement.Address.Region,
                    District = announcement.Address.District,
                    Settlement = announcement.Address.Settlement,
                    Street = announcement.Address.Street,
                    PostalCode = announcement.Address.PostalCode,
                },
                ContactInformation = new ContactInformationDtoWithId
                {
                    Id = announcement.ContactInformation.Id,
                    Email = announcement.ContactInformation.Email,
                    PhoneNumber = announcement.ContactInformation.PhoneNumber,
                    Telegram = announcement.ContactInformation.Telegram,
                    Viber = announcement.ContactInformation.Viber,
                    Facebook = announcement.ContactInformation.Facebook,
                },
                AnnouncementGroups = announcement.Groups
                .Select(g => new AnnouncementGroupDtoWithId()
                {
                    Id = g.Id,
                    Name = g.Name,
                })
                .ToList(),
            };

        public static Func<Announcement, BaseAnnouncementResultWithType> AnnouncementToAnnouncementDtoModerationMapping =>
            announcement =>
            {
                AnnouncementType type = default;

                switch (announcement)
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

                 return new BaseAnnouncementResultWithType
                {
                    Id = announcement.Id,
                    Title = announcement.Title,
                    Content = announcement.Content,
                    CreatedAt = announcement.CreatedAt,
                    IsAccepted = announcement.Accepted,
                    NonAcceptenceReason = announcement.NonAcceptenceReason,
                    ResponsesCount = announcement.Responses?.Count ?? 0,
                    IsClosed = announcement.IsClosed,
                    Author = (announcement.Author != null) ? new UserDtoWithId()
                    {
                        Id = announcement.AuthorId ?? 0,
                        FirstName = announcement.Author.FirstName,
                        LastName = announcement.Author.LastName,
                        CreatedAt = announcement.Author.CreatedAt,
                        DateOfBirth = announcement.Author.DateOfBirth,
                        Email = announcement.Author.Email,
                        PhoneNumber = announcement.Author.PhoneNumber,
                    } : null,
                    Address = (announcement.Address != null) ? new AddressDtoWithId
                    {
                        Id = announcement.Address.Id,
                        Country = announcement.Address.Country,
                        Region = announcement.Address.Region,
                        District = announcement.Address.District,
                        Settlement = announcement.Address.Settlement,
                        Street = announcement.Address.Street,
                        PostalCode = announcement.Address.PostalCode,
                    } : null,
                    ContactInformation = (announcement.ContactInformation != null) ? new ContactInformationDtoWithId
                    {
                        Id = announcement.ContactInformation.Id,
                        Email = announcement.ContactInformation.Email,
                        PhoneNumber = announcement.ContactInformation.PhoneNumber,
                        Telegram = announcement.ContactInformation.Telegram,
                        Viber = announcement.ContactInformation.Viber,
                        Facebook = announcement.ContactInformation.Facebook,
                    } : null,
                    AnnouncementGroups = announcement.Groups
                    .Select(g => new AnnouncementGroupDtoWithId()
                    {
                        Id = g.Id,
                        Name = g.Name,
                    })
                    .ToList(),
                    Type = type
                };
            };
    }
}
