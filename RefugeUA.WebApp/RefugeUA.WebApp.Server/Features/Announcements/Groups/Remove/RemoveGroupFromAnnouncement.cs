
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.WebApp.Server.Authorization.Constants;
using RefugeUA.WebApp.Server.Shared.Dto.BaseAnnouncement;

namespace RefugeUA.WebApp.Server.Features.Announcements.Groups.Remove
{
    public class RemoveGroupFromAnnouncement : IFeatureEndpoint
    {
        public static async Task<IResult> RemoveGroupFromAnnouncementAsync(
            [FromRoute] long id,
            [FromRoute] long groupId,
            [FromServices] RefugeUADbContext dbContext,
            [FromServices] IAuthorizationService authService,
            [FromServices] IHttpContextAccessor httpContextAccessor)
        {
            var announcement = await dbContext.Announcements.
                Include(a => a.Groups).
                Include(a => a.Address).
                FirstOrDefaultAsync(a => a.Id == id);

            if (announcement == null)
            {
                return Results.NotFound("Announcement by id not found.");
            }

            if (!(await authService.AuthorizeAsync(
                httpContextAccessor.HttpContext!.User,
                BaseAnnouncementResultMapping.Func(announcement),
                Policies.EditDeleteAnnouncementPolicy)).Succeeded)
            {
                return Results.Forbid();
            }

            if (!announcement.Groups.Any(g => g.Id == groupId))
            {
                return Results.NotFound("Announcement group by id not found.");
            }

            announcement.Groups.Remove(announcement.Groups.First(g => g.Id == groupId));
            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("api/announcements/{id:long}/groups/{groupId:long}", RemoveGroupFromAnnouncementAsync).
                Produces(StatusCodes.Status204NoContent).
                Produces(StatusCodes.Status400BadRequest).
                Produces(StatusCodes.Status401Unauthorized).
                Produces(StatusCodes.Status403Forbidden).
                WithTags("Announcements").
                WithName("RemoveGroupFromAnnouncement");
        }
    }
}
