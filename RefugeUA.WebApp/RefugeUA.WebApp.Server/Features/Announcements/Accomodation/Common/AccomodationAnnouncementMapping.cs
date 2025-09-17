using RefugeUA.Entities;
using System.Linq.Expressions;

namespace RefugeUA.WebApp.Server.Features.Announcements.Accomodation.Common
{
    public static class AccomodationAnnouncementMapping
    {
        public readonly static Func<AccomodationAnnouncement, AccomodationAnnouncementResult> Func = a => new AccomodationAnnouncementResult()
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
            IsClosed = a.IsClosed,
            AuthorId = a.AuthorId ?? 0,
            Images = a.Images?.Select(i => new ImageResult()
            {
                Path = i.Path,
            }).ToArray(),
        };
    }
}
