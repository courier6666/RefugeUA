
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.WebApp.Server.Authorization.Constants;
using RefugeUA.WebApp.Server.Extensions.Authentication;

namespace RefugeUA.WebApp.Server.Features.Admin.Announcements.Moderation.Reject
{
    public class RejectAnnouncement : IFeatureEndpoint
    {
        [Authorize(Roles = "Admin,CommunityAdmin")]
        public static async Task<IResult> RejectAnnouncementAsync(
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

            if (!foundAnnouncement.Accepted)
            {
                return Results.Problem("Announcement is already rejected.");
            }

            foundAnnouncement.Accepted = false;
            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPatch("api/admin/announcements/{id:long}/moderation/reject", RejectAnnouncementAsync)
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status403Forbidden)
                .Produces(StatusCodes.Status401Unauthorized)
                .Produces(StatusCodes.Status400BadRequest)
                .WithName("RejectAnnouncement")
                .WithTags("AdminAndCommunityAdmin")
                .RequireAuthorization();
        }
    }
}
