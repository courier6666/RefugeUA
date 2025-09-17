
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.WebApp.Server.Authorization.Constants;
using RefugeUA.WebApp.Server.Shared.Dto.BaseAnnouncement;

namespace RefugeUA.WebApp.Server.Features.Announcements.Groups.Add
{
    public class AddGroupToAnnouncement : IFeatureEndpoint
    {
        public static async Task<IResult> AddGroupToAnnouncementAsync(
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

            var announcementGroup = await dbContext.AnnouncementGroups.
                FirstOrDefaultAsync(g => g.Id == groupId);

            if (announcementGroup == null)
            {
                return Results.NotFound("Announcement group by id not found.");
            }

            if (announcement.Groups.Any(g => g.Id == groupId))
            {
                return Results.BadRequest("Announcement group by id is already assigned to announcement.");
            }

            announcement.Groups.Add(announcementGroup);
            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("api/announcements/{id:long}/groups/{groupId:long}", AddGroupToAnnouncementAsync).
                Produces(StatusCodes.Status204NoContent).
                Produces(StatusCodes.Status400BadRequest).
                Produces(StatusCodes.Status401Unauthorized).
                Produces(StatusCodes.Status403Forbidden).
                WithTags("Announcements").
                WithName("AddGroupToAnnouncementt");
        }
    }
}
