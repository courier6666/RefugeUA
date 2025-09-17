
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.WebApp.Server.Authorization.Constants;
using RefugeUA.WebApp.Server.Extensions.Authentication;
using RefugeUA.WebApp.Server.Features.Admin.Announcements.Moderation.SetNonAcceptenceReason;

namespace RefugeUA.WebApp.Server.Features.Admin.Announcements.Moderation.SetAcceptenceReason
{
    public class SetNonAcceptenceReasonAnnouncement : IFeatureEndpoint
    {
        [Authorize(Roles = "Admin,CommunityAdmin")]
        public static async Task<IResult> SetNonAcceptenceReasonAnnouncementAsync(
            [FromRoute] long id,
            [FromBody] ReasonBody reasonBody,
            [FromServices] RefugeUADbContext dbContext,
            [FromServices] IHttpContextAccessor httpContextAccessor)
        {
            string reason = reasonBody.Reason;

            if (string.IsNullOrWhiteSpace(reason))
            {
                return Results.BadRequest("Reason cannot be empty.");
            }

            if (reason.Length > 500)
            {
                return Results.BadRequest("Reason cannot be longer than 500 characters.");
            }

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

            foundAnnouncement.Accepted = false;
            foundAnnouncement.NonAcceptenceReason = reason;
            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPatch("api/admin/announcements/{id:long}/moderation/non-acceptance-reason", SetNonAcceptenceReasonAnnouncementAsync)
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status403Forbidden)
                .Produces(StatusCodes.Status401Unauthorized)
                .Produces(StatusCodes.Status400BadRequest)
                .WithName("SetNonAcceptenceReasonAnnouncement")
                .WithTags("AdminAndCommunityAdmin")
                .RequireAuthorization();
        }
    }
}
