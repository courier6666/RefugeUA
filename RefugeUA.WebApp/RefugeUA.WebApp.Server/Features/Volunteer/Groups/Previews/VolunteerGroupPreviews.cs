
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.WebApp.Server.Features.Volunteer.Groups.Common;

namespace RefugeUA.WebApp.Server.Features.Volunteer.Groups.Previews
{
    public class VolunteerGroupPreviews : IFeatureEndpoint
    {
        public static async Task<IResult> VolunteerGroupPreviewsAsync(
            [FromServices] RefugeUADbContext dbContext)
        {
            var groupPreviews = await dbContext.VolunteerGroups.AsNoTracking().Select(g => new VolunteerGroupPreview()
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
            app.MapGet("api/volunteer/groups/previews", VolunteerGroupPreviewsAsync).
                Produces<List<VolunteerGroupPreview>>(StatusCodes.Status200OK).
                WithName("VolunteerGroupPreviews").
                WithTags("Volunteer");
        }
    }
}
