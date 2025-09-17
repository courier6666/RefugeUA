
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.WebApp.Server.Authorization.Constants;
using RefugeUA.WebApp.Server.Shared.Dto.BaseAnnouncement;

namespace RefugeUA.WebApp.Server.Features.Announcements.Delete
{
    public class DeleteAnnouncement : IFeatureEndpoint
    {
        public static async Task<IResult> DeleteAnnouncementAsync(
            [FromRoute] int id,
            [FromServices] RefugeUADbContext dbContext,
            [FromServices] IAuthorizationService authService,
            [FromServices] IHttpContextAccessor httpContextAccessor)
        {
            var foundAnnouncement = await dbContext.Announcements
                .Include(x => x.Address)
                .Include(x => x.ContactInformation)
                .Include(x => x.Responses)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (foundAnnouncement == null)
            {
                return Results.NotFound();
            }

            if (!(await authService.AuthorizeAsync(
                httpContextAccessor.HttpContext!.User,
                BaseAnnouncementResultMapping.Func(foundAnnouncement),
                Policies.EditDeleteAnnouncementPolicy)).Succeeded)
            {
                return Results.Forbid();
            }

            dbContext.Announcements.Remove(foundAnnouncement);
            dbContext.Addresses.Remove(foundAnnouncement.Address);
            dbContext.ContactInformation.Remove(foundAnnouncement.ContactInformation);
            dbContext.AnnouncementResponses.RemoveRange(foundAnnouncement.Responses);

            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("api/announcements/{id:long}", DeleteAnnouncementAsync)
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status403Forbidden)
                .Produces(StatusCodes.Status401Unauthorized)
                .WithName("DeleteAnnouncement")
                .WithTags("Announcements")
                .RequireAuthorization();
        }
    }
}
