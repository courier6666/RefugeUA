using RefugeUA.Entities;
using RefugeUA.WebApp.Server.Features.Announcements.Common;
using RefugeUA.WebApp.Server.Features.Announcements.Education.Common;
using RefugeUA.WebApp.Server.Shared.Dto.Address;
using RefugeUA.WebApp.Server.Shared.Dto.ContactInformation;
using RefugeUA.WebApp.Server.Shared.Dto.User;
using System.Linq.Expressions;

namespace RefugeUA.WebApp.Server.Features.Announcements.Education.Detail
{
    public static class DetailEducationAnnouncementMapping
    {
        public static Expression<Func<EducationAnnouncement, EducationAnnouncementResult>> Expression => a => new EducationAnnouncementResult()
        {
            Id = a.Id,
            Title = a.Title,
            Content = a.Content,
            CreatedAt = a.CreatedAt,
            Fee = a.Fee,
            Duration = a.Duration,
            Language = a.Language,
            EducationType = a.EducationType,
            TargetGroups = new[] { a.TargetGroup },
            InstitutionName = a.InstitutionName,
            IsAccepted = a.Accepted,
            NonAcceptenceReason = a.NonAcceptenceReason,
            Author = new UserDtoWithId()
            {
                Id = a.AuthorId ?? 0,
                FirstName = a.Author.FirstName,
                LastName = a.Author.LastName,
                CreatedAt = a.Author.CreatedAt,
                DateOfBirth = a.Author.DateOfBirth,
                Email = a.Author.Email,
                PhoneNumber = a.Author.PhoneNumber,
            },
            Address = new AddressDtoWithId()
            {
                Id = a.AddressId,
                Country = a.Address.Country,
                Region = a.Address.Region,
                District = a.Address.District,
                Settlement = a.Address.Settlement,
                Street = a.Address.Street,
                PostalCode = a.Address.PostalCode,
            },
            ContactInformation = new ContactInformationDtoWithId()
            {
                PhoneNumber = a.ContactInformation.PhoneNumber,
                Email = a.ContactInformation.Email,
                Telegram = a.ContactInformation.Telegram,
                Viber = a.ContactInformation.Viber,
                Facebook = a.ContactInformation.Facebook,
            },
            ResponsesCount = a.Responses.Count,
            AnnouncementGroups = a.Groups
            .Select(g => new AnnouncementGroupDtoWithId()
            {
                Id = g.Id,
                Name = g.Name,
            })
            .ToList(),
            IsClosed = a.IsClosed
        };
    }
}
