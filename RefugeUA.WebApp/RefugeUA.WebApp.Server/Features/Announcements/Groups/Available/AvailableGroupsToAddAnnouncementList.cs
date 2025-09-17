using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.WebApp.Server.Features.Announcements.Common;

namespace RefugeUA.WebApp.Server.Features.Announcements.Groups.Available
{
    public class AvailableGroupsToAddAnnouncementList : IFeatureEndpoint
    {
        public static async Task<IResult> AvailableGroupsToAddAnnouncementListAsync(
            [FromRoute] long id,
            [FromServices] RefugeUADbContext dbContext)
        {
            var announcement = await dbContext.Announcements.
                Include(a => a.Groups).
                FirstOrDefaultAsync(a => a.Id == id);

            if (announcement == null)
            {
                return Results.NotFound("Announcement by id not found.");
            }

            var availableGroups = await dbContext.AnnouncementGroups.
                Where(g => !announcement.Groups.Select(g1 => g1.Id).Contains(g.Id)).
                OrderByDescending(g => g.Name).
                Select(g => new AnnouncementGroupDtoWithId()
                {
                    Id = g.Id,
                    Name = g.Name,
                }).
                ToListAsync();

            return Results.Ok(availableGroups);
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("api/announcements/{id:long}/groups/available", AvailableGroupsToAddAnnouncementListAsync).
                Produces<List<AnnouncementGroupDtoWithId>>().
                Produces(StatusCodes.Status200OK).
                Produces(StatusCodes.Status404NotFound);
        }
    }
}
