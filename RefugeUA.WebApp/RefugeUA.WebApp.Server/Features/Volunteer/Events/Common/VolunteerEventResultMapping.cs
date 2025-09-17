using RefugeUA.Entities;
using RefugeUA.WebApp.Server.Shared.Dto.Address;
using RefugeUA.WebApp.Server.Shared.Dto.User;
using System.Linq.Expressions;

namespace RefugeUA.WebApp.Server.Features.Volunteer.Events.Common
{
    public static class VolunteerEventResultMapping
    {

        public readonly static Expression<Func<VolunteerEvent, VolunteerEventBaseResult>> BaseResultExpression =
            r => new VolunteerEventBaseResult()
            {
                Id = r.Id,
                Title = r.Title,
                Content = r.Content,
                StartTime = r.StartTime,
                EndTime = r.EndTime,
                CreatedAt = r.CreatedAt,
                AddressId = r.AddressId,
                Address = r.Address == null ? null : new AddressDtoWithId()
                {
                    Id = r.AddressId ?? 0,
                    Country = r.Address.Country,
                    Region = r.Address.Region,
                    District = r.Address.District,
                    Settlement = r.Address.Settlement,
                    Street = r.Address.Street,
                    PostalCode = r.Address.PostalCode,
                },
                VolunteerGroupId = r.VolunteerGroupId,
                VolunteerGroupTitle = r.VolunteerGroup.Title,
                OnlineConferenceLink = r.OnlineConferenceLink,
                DonationLink = r.DonationLink,
                IsClosed = r.IsClosed,
                EventType = r.EventType,
                Organizers = r.Organizers.Select(o => new UserDtoWithId()
                {
                    Id = o.Id,
                    FirstName = o.FirstName,
                    LastName = o.LastName,
                    CreatedAt = o.CreatedAt,
                    DateOfBirth = o.DateOfBirth,
                    Email = o.Email,
                    PhoneNumber = o.PhoneNumber,
                }),
                ParticipantsCount = r.Participants.Count
            };

        public readonly static Func<VolunteerEvent, VolunteerEventBaseResult> BaseResultFunc =
            r => new VolunteerEventBaseResult()
            {
                Id = r.Id,
                Title = r.Title,
                Content = r.Content,
                StartTime = r.StartTime,
                EndTime = r.EndTime,
                CreatedAt = r.CreatedAt,
                AddressId = r.AddressId,
                Address = r.Address == null ? null : new AddressDtoWithId()
                {
                    Id = r.AddressId ?? 0,
                    Country = r.Address.Country,
                    Region = r.Address.Region,
                    District = r.Address.District,
                    Settlement = r.Address.Settlement,
                    Street = r.Address.Street,
                    PostalCode = r.Address.PostalCode,
                },
                VolunteerGroupId = r.VolunteerGroupId,
                VolunteerGroupTitle = r.VolunteerGroup?.Title,
                OnlineConferenceLink = r.OnlineConferenceLink,
                DonationLink = r.DonationLink,
                IsClosed = r.IsClosed,
                EventType = r.EventType,
                Organizers = r.Organizers.Select(o => new UserDtoWithId()
                {
                    Id = o.Id,
                    FirstName = o.FirstName,
                    LastName = o.LastName,
                    CreatedAt = o.CreatedAt,
                    DateOfBirth = o.DateOfBirth,
                    Email = o.Email,
                    PhoneNumber = o.PhoneNumber,
                }),
                ParticipantsCount = r.Participants?.Count ?? 0
            };

        public readonly static Expression<Func<VolunteerEvent, VolunteerEventDetailResult>> DetailResultExpression =
            r => new VolunteerEventDetailResult()
            {
                Id = r.Id,
                Title = r.Title,
                Content = r.Content,
                StartTime = r.StartTime,
                EndTime = r.EndTime,
                CreatedAt = r.CreatedAt,
                AddressId = r.AddressId,
                Address = r.Address == null ? null : new AddressDtoWithId()
                {
                    Id = r.AddressId ?? 0,
                    Country = r.Address.Country,
                    Region = r.Address.Region,
                    District = r.Address.District,
                    Settlement = r.Address.Settlement,
                    Street = r.Address.Street,
                    PostalCode = r.Address.PostalCode,
                },
                VolunteerGroupId = r.VolunteerGroupId,
                VolunteerGroupTitle = r.VolunteerGroup.Title,
                OnlineConferenceLink = r.OnlineConferenceLink,
                DonationLink = r.DonationLink,
                IsClosed = r.IsClosed,
                EventType = r.EventType,
                Organizers = r.Organizers.Select(o => new UserDtoWithId()
                {
                    Id = o.Id,
                    FirstName = o.FirstName,
                    LastName = o.LastName,
                    CreatedAt = o.CreatedAt,
                    DateOfBirth = o.DateOfBirth,
                    Email = o.Email,
                    PhoneNumber = o.PhoneNumber,
                }),
                ParticipantsCount = r.Participants.Count,
                ScheduleItems = r.ScheduleItems.Select(i => new VolunteerEventScheduleItemDtoWithId()
                {
                    Id = i.Id,
                    Description = i.Description,
                    StartTime = i.StartTime,
                })
            };

        public readonly static Func<VolunteerEvent, VolunteerEventDetailResult> DetailResultFunc =
            r => new VolunteerEventDetailResult()
            {
                Id = r.Id,
                Title = r.Title,
                Content = r.Content,
                StartTime = r.StartTime,
                EndTime = r.EndTime,
                CreatedAt = r.CreatedAt,
                AddressId = r.AddressId,
                Address = r.Address == null ? null : new AddressDtoWithId()
                {
                    Id = r.AddressId ?? 0,
                    Country = r.Address.Country,
                    Region = r.Address.Region,
                    District = r.Address.District,
                    Settlement = r.Address.Settlement,
                    Street = r.Address.Street,
                    PostalCode = r.Address.PostalCode,
                },
                VolunteerGroupId = r.VolunteerGroupId,
                VolunteerGroupTitle = r.VolunteerGroup?.Title,
                OnlineConferenceLink = r.OnlineConferenceLink,
                DonationLink = r.DonationLink,
                IsClosed = r.IsClosed,
                EventType = r.EventType,
                Organizers = r.Organizers.Select(o => new UserDtoWithId()
                {
                    Id = o.Id,
                    FirstName = o.FirstName,
                    LastName = o.LastName,
                    CreatedAt = o.CreatedAt,
                    DateOfBirth = o.DateOfBirth,
                    Email = o.Email,
                    PhoneNumber = o.PhoneNumber,
                }),
                ParticipantsCount = r.Participants?.Count ?? 0,
                ScheduleItems = r.ScheduleItems.Select(i => new VolunteerEventScheduleItemDtoWithId()
                {
                    Id = i.Id,
                    Description = i.Description,
                    StartTime = i.StartTime,
                })
            };
    }
}
