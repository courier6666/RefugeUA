
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.WebApp.Server.Authorization.Constants;
using RefugeUA.WebApp.Server.Extensions.Authentication;

namespace RefugeUA.WebApp.Server.Features.Admin.Announcements.Moderation.Accept
{
    public class AcceptAnnouncement : IFeatureEndpoint
    {
        [Authorize(Roles = "Admin,CommunityAdmin")]
        public static async Task<IResult> AcceptAnnouncementAsync(
            [FromRoute] long id,
            [FromServices] RefugeUADbContext dbContext,
            [FromServices] IHttpContextAccessor httpContextAccessor)
        {
            var user = httpContextAccessor!.HttpContext!.User;

            var foundAnnouncement = await dbContext.Announcements
                .Include(a => a.Address)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (foundAnnouncement == null)
            {
                return Results.NotFound();
            }

            if (user.IsInRole(Roles.CommunityAdmin) && user.GetDistrict() != foundAnnouncement.Address.District)
            {
                return Results.Forbid();
            }

            if (foundAnnouncement.Accepted)
            {
                return Results.Problem("Announcement is already accepted.");
            }

            foundAnnouncement.Accepted = true;
            foundAnnouncement.NonAcceptenceReason = null;
            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPatch("api/admin/announcements/{id:long}/moderation/accept", AcceptAnnouncementAsync)
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status403Forbidden)
                .Produces(StatusCodes.Status401Unauthorized)
                .Produces(StatusCodes.Status400BadRequest)
                .WithName("AcceptAnnouncement")
                .WithTags("AdminAndCommunityAdmin")
                .RequireAuthorization();
        }
    }
}
