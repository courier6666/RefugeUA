
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.WebApp.Server.Features.Announcements.Common;

namespace RefugeUA.WebApp.Server.Features.Announcements.Groups.Added
{
    public class AddedGroupsToAnnouncementList : IFeatureEndpoint
    {
        public static async Task<IResult> AddedGroupsToAnnouncementListAsync(
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

            return Results.Ok(announcement.Groups.OrderByDescending(g => g.Name).Select(g => new AnnouncementGroupDtoWithId()
            {
                Id = g.Id,
                Name = g.Name,
            }));
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("api/announcements/{id:long}/groups/added", AddedGroupsToAnnouncementListAsync).
                Produces<List<AnnouncementGroupDtoWithId>>().
                Produces(StatusCodes.Status200OK).
                Produces(StatusCodes.Status404NotFound);
        }
    }
}
