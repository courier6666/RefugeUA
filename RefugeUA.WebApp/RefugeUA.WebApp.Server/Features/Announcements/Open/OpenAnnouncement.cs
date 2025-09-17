using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.WebApp.Server.Authorization.Constants;
using RefugeUA.WebApp.Server.Shared.Dto.BaseAnnouncement;

namespace RefugeUA.WebApp.Server.Features.Announcements.Open
{
    public class OpenAnnouncement : IFeatureEndpoint
    {
        public static async Task<IResult> OpenAnnouncementAsync([FromRoute] long id,
            [FromServices] RefugeUADbContext dbContext,
            [FromServices] IHttpContextAccessor httpContextAccessor,
            [FromServices] IAuthorizationService authService)
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

            foundAnnouncement.IsClosed = false;
            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPatch("api/announcements/{id:long}/open", OpenAnnouncementAsync)
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                .WithName("OpenAnnouncement")
                .WithTags("Announcements")
                .RequireAuthorization();
        }
    }
}
