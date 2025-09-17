using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;

namespace RefugeUA.WebApp.Server.Features.Announcements.Groups.Delete
{
    public class DeleteAnnouncementGroupByName : IFeatureEndpoint
    {
        [Authorize(Roles = "Admin,CommunityAdmin")]
        public static async Task<IResult> DeleteAnnouncementGroupByNameAsync(
            [FromQuery] string name,
            [FromServices] RefugeUADbContext dbContext)
        {
            var announcementGroup = await dbContext.AnnouncementGroups.
                FirstOrDefaultAsync(g => g.Name == name);

            if (announcementGroup == null)
            {
                return Results.NotFound();
            }

            dbContext.AnnouncementGroups.Remove(announcementGroup);
            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("api/announcements/groups", DeleteAnnouncementGroupByNameAsync).
                Produces(StatusCodes.Status204NoContent).
                Produces(StatusCodes.Status400BadRequest).
                Produces(StatusCodes.Status401Unauthorized).
                Produces(StatusCodes.Status403Forbidden).
                WithTags("Announcements").
                WithName("DeleteAnnouncementGroupByName");
        }
    }
}
