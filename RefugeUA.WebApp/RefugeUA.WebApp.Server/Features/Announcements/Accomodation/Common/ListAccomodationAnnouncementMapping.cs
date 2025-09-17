using RefugeUA.Entities;
using RefugeUA.WebApp.Server.Features.Announcements.Common;
using RefugeUA.WebApp.Server.Features.Announcements.Accomodation.Common;
using RefugeUA.WebApp.Server.Shared.Dto.Address;
using RefugeUA.WebApp.Server.Shared.Dto.User;
using System.Linq.Expressions;

namespace RefugeUA.WebApp.Server.Features.Announcements.Accomodation.Common
{
    public static class ListAccomodationAnnouncementMapping
    {
        public static Expression<Func<AccomodationAnnouncement, AccomodationAnnouncementResult>> Expression => a => new AccomodationAnnouncementResult()
        {
            Id = a.Id,
            Title = a.Title,
            Content = a.Content,
            CreatedAt = a.CreatedAt,
            BuildingType = a.BuildingType,
            NumberOfRooms = a.NumberOfRooms,
            Floors = a.Floors,
            PetsAllowed = a.PetsAllowed,
            Capacity = a.Capacity,
            AreaSqMeters = a.AreaSqMeters,
            Price = a.Price,
            IsFree = a.IsFree,
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
            ResponsesCount = a.Responses.Count,
            AnnouncementGroups = a.Groups
                .Select(g => new AnnouncementGroupDtoWithId()
                {
                    Id = g.Id,
                    Name = g.Name,
                })
                .ToList(),
            IsClosed = a.IsClosed,
            Images = a.Images.Select(i => new ImageResult()
            {
                Path = i.Path,
            }).ToArray(),
        };
    }
}
