using RefugeUA.Entities;
using System.Linq.Expressions;

namespace RefugeUA.WebApp.Server.Features.Volunteer.Groups.Common
{
    public static class VolunteerGroupResultMapping
    {
        public readonly static Expression<Func<VolunteerGroup, VolunteerGroupResult>> Expression =
            g => new VolunteerGroupResult()
            {
                Id = g.Id,
                Title = g.Title,
                DescriptionContent = g.DescriptionContent,
                CreatedAt = g.CreatedAt,
                AdministratorsCount = g.Admins.Count,
                FollowersCount = g.Followers.Count,
                VolunteerEventsCount = g.VolunteerEvents.Count,
            };

        public readonly static Func<VolunteerGroup, VolunteerGroupResult> Func =
            g => new VolunteerGroupResult()
            {
                Id = g.Id,
                Title = g.Title,
                DescriptionContent = g.DescriptionContent,
                CreatedAt = g.CreatedAt,
                AdministratorsCount = g.Admins?.Count,
                FollowersCount = g.Followers?.Count,
                VolunteerEventsCount = g.VolunteerEvents?.Count,
            };
    }
}
