
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.WebApp.Server.Extensions.Authentication;
using RefugeUA.WebApp.Server.Features.Volunteer.Groups.Common;

namespace RefugeUA.WebApp.Server.Features.Volunteer.Groups.Previews.Available
{
    public class AvaillableVolunteerGroupsPreviews : IFeatureEndpoint
    {
        public static async Task<IResult> AvaillableVolunteerGroupsPreviewsAsync(
            [FromServices] RefugeUADbContext dbContext,
            [FromServices] IHttpContextAccessor httpContextAccessor)
        {
            var userId = httpContextAccessor.HttpContext?.User.GetId() ?? 0;

            var groupPreviews = await dbContext.VolunteerGroups.AsNoTracking().
                Where(g => g.Followers.Any(f => f.Id == userId) || g.Admins.Any(a => a.Id == userId)).
                Select(g => new VolunteerGroupPreview()
            {
                Id = g.Id,
                Title = g.Title,
                AdministratorsCount = g.Admins.Count,
                FollowersCount = g.Followers.Count,
                VolunteerEventsCount = g.VolunteerEvents.Count,
            }).ToListAsync();

            return Results.Ok(groupPreviews);
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("api/volunteer/groups/previews/available", AvaillableVolunteerGroupsPreviewsAsync).
                Produces<List<VolunteerGroupPreview>>(StatusCodes.Status200OK).
                Produces(StatusCodes.Status401Unauthorized).
                WithName("AvailableVolunteerGroupPreviews").
                WithTags("Volunteer").
                RequireAuthorization();
        }
    }
}
