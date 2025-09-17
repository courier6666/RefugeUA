using RefugeUA.DatabaseAccess.Identity;
using RefugeUA.Entities;
using RefugeUA.WebApp.Server.Features.MentalSupport.Articles.Common;
using RefugeUA.WebApp.Server.Shared.Dto.ContactInformation;
using RefugeUA.WebApp.Server.Shared.Dto.User;
using System.Linq.Expressions;

namespace RefugeUA.WebApp.Server.Features.MentalSupport.SpecialistsInfos.Common
{
    public static class PsychologistInformationResultMapping
    {
        public static readonly Expression<Func<PsychologistInformation, PsychologistInformationResult>> Expression =
            a => new PsychologistInformationResult()
            {
                Id = a.Id,
                Title = a.Title,
                Description = a.Description,
                CreatedAt = a.CreatedAt,
                AuthorId = a.AuthorId ?? 0,
                Contact = new ContactInformationDtoWithId()
                {
                    Id = a.Contact.Id,
                    Email = a.Contact.Email,
                    Facebook = a.Contact.Facebook,
                    PhoneNumber = a.Contact.PhoneNumber,
                    Telegram = a.Contact.Telegram,
                    Viber = a.Contact.Viber,
                },
                Author = new UserDtoWithId()
                {
                    Id = a.Author.Id,
                    CreatedAt = a.Author.CreatedAt,
                    DateOfBirth = a.Author.DateOfBirth,
                    Email = a.Author.Email,
                    FirstName = a.Author.FirstName,
                    LastName = a.Author.LastName,
                    PhoneNumber = a.Author.PhoneNumber,
                }
            };

        public static readonly Func<PsychologistInformation, PsychologistInformationResult> Func =
            a => new PsychologistInformationResult()
            {
                Id = a.Id,
                Title = a.Title,
                Description = a.Description,
                CreatedAt = a.CreatedAt,
                AuthorId = a.AuthorId ?? 0,
                Contact = new ContactInformationDtoWithId()
                {
                    Id = a.Contact.Id,
                    Email = a.Contact.Email,
                    Facebook = a.Contact.Facebook,
                    PhoneNumber = a.Contact.PhoneNumber,
                    Telegram = a.Contact.Telegram,
                    Viber = a.Contact.Viber,
                },
                Author = new UserDtoWithId()
                {
                    Id = a.Author.Id,
                    CreatedAt = a.Author.CreatedAt,
                    DateOfBirth = a.Author.DateOfBirth,
                    Email = a.Author.Email,
                    FirstName = a.Author.FirstName,
                    LastName = a.Author.LastName,
                    PhoneNumber = a.Author.PhoneNumber,
                }
            };
    }
}
